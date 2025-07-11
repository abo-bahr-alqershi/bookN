using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.FieldTypes;
using YemenBooking.Application.Exceptions;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Handlers.Commands.FieldTypes
{
    /// <summary>
    /// معالج أمر تغيير حالة التفعيل لنوع الحقل
    /// Toggle active status of a field type
    /// </summary>
    public class ToggleFieldTypeStatusCommandHandler : IRequestHandler<ToggleFieldTypeStatusCommand, ResultDto<bool>>
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly IEventPublisher _eventPublisher;
        private readonly ILogger<ToggleFieldTypeStatusCommandHandler> _logger;

        public ToggleFieldTypeStatusCommandHandler(
            IFieldTypeRepository fieldTypeRepository,
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            IEventPublisher eventPublisher,
            ILogger<ToggleFieldTypeStatusCommandHandler> logger)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _eventPublisher = eventPublisher;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(ToggleFieldTypeStatusCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تغيير حالة نوع الحقل {FieldTypeId} إلى {IsActive}", request.FieldTypeId, request.IsActive);

            if (request.FieldTypeId == Guid.Empty)
                throw new BusinessRuleException("InvalidFieldTypeId", "معرف نوع الحقل غير صالح");

            var existing = await _fieldTypeRepository.GetFieldTypeByIdAsync(request.FieldTypeId, cancellationToken);
            if (existing == null)
                throw new NotFoundException("FieldType", $"$(request.FieldTypeId)", "نوع الحقل غير موجود");

            if (_currentUserService.Role != "Admin")
                throw new ForbiddenException("غير مصرح لك بتغيير حالة نوع الحقل");

            await _unitOfWork.ExecuteInTransactionAsync(async () =>
            {
                existing.IsActive = request.IsActive;
                existing.UpdatedBy = _currentUserService.UserId;
                existing.UpdatedAt = DateTime.UtcNow;
                await _fieldTypeRepository.UpdateFieldTypeAsync(existing, cancellationToken);

                await _auditService.LogActivityAsync(
                    "FieldType",
                    existing.Id.ToString(),
                    "ToggleStatus",
                    $"تم تغيير حالة نوع الحقل إلى {(request.IsActive ? "مفعّل" : "معطّل")}",
                    null,
                    null,
                    cancellationToken);

                // await _eventPublisher.PublishEventAsync(new FieldTypeStatusToggledEvent
                // {
                //     FieldTypeId = existing.Id,
                //     IsActive = request.IsActive,
                //     ToggledBy = _currentUserService.UserId,
                //     ToggledAt = DateTime.UtcNow
                // }, cancellationToken);

                _logger.LogInformation("تم تغيير حالة نوع الحقل بنجاح: {FieldTypeId}", existing.Id);
            });

            return ResultDto<bool>.Ok(true, 
                request.IsActive ? "تم تفعيل نوع الحقل بنجاح" : "تم تعطيل نوع الحقل بنجاح");
        }
    }

    /// <summary>
    /// حدث تغيير حالة تفعيل نوع الحقل
    /// Field type status toggled event
    /// </summary>
    public class FieldTypeStatusToggledEvent
    {
        public Guid FieldTypeId { get; set; }
        public bool IsActive { get; set; }
        public Guid ToggledBy { get; set; }
        public DateTime ToggledAt { get; set; }
    }
} 