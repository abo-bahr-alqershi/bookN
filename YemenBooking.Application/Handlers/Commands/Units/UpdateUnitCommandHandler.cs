using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Units;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Application.Handlers.Commands.Units
{
    /// <summary>
    /// معالج أمر تحديث بيانات الوحدة
    /// </summary>
    public class UpdateUnitCommandHandler : IRequestHandler<UpdateUnitCommand, ResultDto<bool>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<UpdateUnitCommandHandler> _logger;

        public UpdateUnitCommandHandler(
            IUnitRepository unitRepository,
            IPropertyRepository propertyRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<UpdateUnitCommandHandler> logger)
        {
            _unitRepository = unitRepository;
            _propertyRepository = propertyRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(UpdateUnitCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث بيانات الوحدة: UnitId={UnitId}", request.UnitId);

            // التحقق من المدخلات
            if (request.UnitId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف الوحدة مطلوب");
            if (request.Name != null && string.IsNullOrWhiteSpace(request.Name))
                return ResultDto<bool>.Failed("اسم الوحدة المطلوب غير صالح");
            if (request.BasePrice != null && request.BasePrice.Amount <= 0)
                return ResultDto<bool>.Failed("السعر الأساسي يجب أن يكون أكبر من صفر");

            // التحقق من الوجود
            var unit = await _unitRepository.GetUnitByIdAsync(request.UnitId, cancellationToken);
            if (unit == null)
                return ResultDto<bool>.Failed("الوحدة غير موجودة");

            // التحقق من الصلاحيات (مالك العقار أو مسؤول)
            var property = await _propertyRepository.GetPropertyByIdAsync(unit.PropertyId, cancellationToken);
            if (property == null)
                return ResultDto<bool>.Failed("العقار المرتبط بالوحدة غير موجود");
            if (_currentUserService.Role != "Admin" && property.OwnerId != _currentUserService.UserId)
                return ResultDto<bool>.Failed("غير مصرح لك بتحديث بيانات هذه الوحدة");

            // التحقق من التكرار عند تغيير الاسم
            if (!string.IsNullOrWhiteSpace(request.Name) && !string.Equals(unit.Name, request.Name.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                bool duplicate = await _unitRepository.ExistsAsync(u => u.PropertyId == unit.PropertyId && u.Name.Trim() == request.Name.Trim() && u.Id != request.UnitId, cancellationToken);
                if (duplicate)
                    return ResultDto<bool>.Failed("يوجد وحدة أخرى بنفس الاسم في هذا العقار");
                unit.Name = request.Name.Trim();
            }

            // تطبيق التحديثات الممكنة
            if (request.BasePrice != null)
                unit.BasePrice = new Money(request.BasePrice.Amount, request.BasePrice.Currency);
            if (!string.IsNullOrWhiteSpace(request.CustomFeatures))
                unit.CustomFeatures = request.CustomFeatures.Trim();
            if (request.PricingMethod.HasValue)
                unit.PricingMethod = request.PricingMethod.Value;

            unit.UpdatedBy = _currentUserService.UserId;
            unit.UpdatedAt = DateTime.UtcNow;

            await _unitRepository.UpdateUnitAsync(unit, cancellationToken);

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "UpdateUnit",
                $"تم تحديث بيانات الوحدة {request.UnitId}",
                request.UnitId,
                "Unit",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل تحديث بيانات الوحدة بنجاح: UnitId={UnitId}", request.UnitId);
            return ResultDto<bool>.Succeeded(true, "تم تحديث بيانات الوحدة بنجاح");
        }
    }
} 