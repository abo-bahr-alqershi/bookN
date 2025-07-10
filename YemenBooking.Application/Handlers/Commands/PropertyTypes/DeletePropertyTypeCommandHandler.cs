using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.PropertyTypes;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;

namespace YemenBooking.Application.Handlers.Commands.PropertyTypes
{
    /// <summary>
    /// معالج أمر حذف نوع العقار
    /// </summary>
    public class DeletePropertyTypeCommandHandler : IRequestHandler<DeletePropertyTypeCommand, ResultDto<bool>>
    {
        private readonly IPropertyTypeRepository _repository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<DeletePropertyTypeCommandHandler> _logger;

        public DeletePropertyTypeCommandHandler(
            IPropertyTypeRepository repository,
            IPropertyRepository propertyRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<DeletePropertyTypeCommandHandler> logger)
        {
            _repository = repository;
            _propertyRepository = propertyRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(DeletePropertyTypeCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف نوع العقار: Id={PropertyTypeId}", request.PropertyTypeId);

            // التحقق من المدخلات
            if (request.PropertyTypeId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف نوع العقار مطلوب");

            // التحقق من الصلاحيات (مسؤول)
            if (_currentUserService.Role != "Admin")
                return ResultDto<bool>.Failed("غير مصرح لك بحذف نوع العقار");

            // التحقق من الوجود
            var type = await _repository.GetPropertyTypeByIdAsync(request.PropertyTypeId, cancellationToken);
            if (type == null)
                return ResultDto<bool>.Failed("نوع العقار غير موجود");

            // التحقق من عدم وجود عقارات مرتبطة
            bool hasProperties = await _propertyRepository.ExistsAsync(p => p.TypeId == request.PropertyTypeId, cancellationToken);
            if (hasProperties)
                return ResultDto<bool>.Failed("لا يمكن حذف نوع العقار لوجود عقارات مرتبطة به");

            // تنفيذ الحذف
            var success = await _repository.DeletePropertyTypeAsync(request.PropertyTypeId, cancellationToken);
            if (!success)
                return ResultDto<bool>.Failed("فشل حذف نوع العقار");

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "DeletePropertyType",
                $"تم حذف نوع العقار {request.PropertyTypeId}",
                request.PropertyTypeId,
                "PropertyType",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل حذف نوع العقار: Id={PropertyTypeId}", request.PropertyTypeId);
            return ResultDto<bool>.Succeeded(true, "تم حذف نوع العقار بنجاح");
        }
    }
} 