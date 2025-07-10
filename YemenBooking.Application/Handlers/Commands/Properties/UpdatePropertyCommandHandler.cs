using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Properties;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;

namespace YemenBooking.Application.Handlers.Commands.Properties
{
    /// <summary>
    /// معالج أمر تحديث بيانات العقار
    /// </summary>
    public class UpdatePropertyCommandHandler : IRequestHandler<UpdatePropertyCommand, ResultDto<bool>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<UpdatePropertyCommandHandler> _logger;

        public UpdatePropertyCommandHandler(
            IPropertyRepository propertyRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<UpdatePropertyCommandHandler> logger)
        {
            _propertyRepository = propertyRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(UpdatePropertyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث بيانات العقار: PropertyId={PropertyId}", request.PropertyId);

            // التحقق من صحة المدخلات
            if (request.PropertyId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف العقار مطلوب");

            // التحقق من وجود العقار
            var property = await _propertyRepository.GetPropertyByIdAsync(request.PropertyId, cancellationToken);
            if (property == null)
                return ResultDto<bool>.Failed("العقار غير موجود");

            // التحقق من الصلاحيات (مالك العقار أو مسؤول)
            if (_currentUserService.Role != "Admin" && property.OwnerId != _currentUserService.UserId)
                return ResultDto<bool>.Failed("غير مصرح لك بتحديث هذا العقار");

            // إذا كان العقار معتمدًا وتم تعديل بيانات حساسة، إعادة تعيين الموافقة
            bool requiresReapproval = property.IsApproved &&
                (!string.IsNullOrWhiteSpace(request.Name) && request.Name != property.Name ||
                 !string.IsNullOrWhiteSpace(request.Address) && request.Address != property.Address ||
                 request.StarRating.HasValue && request.StarRating.Value != property.StarRating);
            if (requiresReapproval)
                property.IsApproved = false;

            // تنفيذ التحديث
            if (!string.IsNullOrWhiteSpace(request.Name))
                property.Name = request.Name;
            if (!string.IsNullOrWhiteSpace(request.Address))
                property.Address = request.Address;
            if (!string.IsNullOrWhiteSpace(request.Description))
                property.Description = request.Description;
            if (!string.IsNullOrWhiteSpace(request.City))
                property.City = request.City;
            if (request.StarRating.HasValue)
                property.StarRating = request.StarRating.Value;
            if (request.Latitude.HasValue && request.Latitude.Value >= -90 && request.Latitude.Value <= 90)
                property.Latitude = (decimal)request.Latitude.Value;
            if (request.Longitude.HasValue && request.Longitude.Value >= -180 && request.Longitude.Value <= 180)
                property.Longitude = (decimal)request.Longitude.Value;

            property.UpdatedBy = _currentUserService.UserId;
            property.UpdatedAt = DateTime.UtcNow;

            await _propertyRepository.UpdatePropertyAsync(property, cancellationToken);

            // تسجيل العملية في سجل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "UpdateProperty",
                $"تم تحديث بيانات العقار {request.PropertyId}",
                request.PropertyId,
                "Property",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل تحديث بيانات العقار: PropertyId={PropertyId}", request.PropertyId);
            return ResultDto<bool>.Succeeded(true, "تم تحديث بيانات العقار بنجاح");
        }
    }
} 