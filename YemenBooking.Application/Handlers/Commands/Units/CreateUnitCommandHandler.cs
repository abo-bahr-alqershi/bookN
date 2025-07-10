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
    /// معالج أمر إنشاء وحدة جديدة في العقار
    /// </summary>
    public class CreateUnitCommandHandler : IRequestHandler<CreateUnitCommand, ResultDto<Guid>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUnitTypeRepository _unitTypeRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateUnitCommandHandler> _logger;

        public CreateUnitCommandHandler(
            IUnitRepository unitRepository,
            IPropertyRepository propertyRepository,
            IUnitTypeRepository unitTypeRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<CreateUnitCommandHandler> logger)
        {
            _unitRepository = unitRepository;
            _propertyRepository = propertyRepository;
            _unitTypeRepository = unitTypeRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<Guid>> Handle(CreateUnitCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء وحدة في العقار: PropertyId={PropertyId}, Name={Name}", request.PropertyId, request.Name);

            // التحقق من المدخلات
            if (request.PropertyId == Guid.Empty)
                return ResultDto<Guid>.Failed("معرف العقار مطلوب");
            if (request.UnitTypeId == Guid.Empty)
                return ResultDto<Guid>.Failed("معرف نوع الوحدة مطلوب");
            if (string.IsNullOrWhiteSpace(request.Name))
                return ResultDto<Guid>.Failed("اسم الوحدة مطلوب");
            if (request.BasePrice == null || request.BasePrice.Amount <= 0)
                return ResultDto<Guid>.Failed("السعر الأساسي يجب أن يكون أكبر من صفر");

            // التحقق من وجود العقار والنوع
            var property = await _propertyRepository.GetPropertyByIdAsync(request.PropertyId, cancellationToken);
            if (property == null)
                return ResultDto<Guid>.Failed("العقار غير موجود");
            var unitType = await _unitTypeRepository.GetUnitTypeByIdAsync(request.UnitTypeId, cancellationToken);
            if (unitType == null)
                return ResultDto<Guid>.Failed("نوع الوحدة غير موجود");

            // التحقق من الصلاحيات (مالك العقار أو مسؤول)
            if (_currentUserService.Role != "Admin" && property.OwnerId != _currentUserService.UserId)
                return ResultDto<Guid>.Failed("غير مصرح لك بإنشاء وحدة في هذا العقار");

            // التحقق من التكرار
            bool exists = await _unitRepository.ExistsAsync(u => u.PropertyId == request.PropertyId && u.Name.Trim() == request.Name.Trim(), cancellationToken);
            if (exists)
                return ResultDto<Guid>.Failed("يوجد وحدة بنفس الاسم في هذا العقار");

            // إنشاء الكيان
            var unit = new global::YemenBooking.Core.Entities.Unit
            {
                PropertyId = request.PropertyId,
                UnitTypeId = request.UnitTypeId,
                Name = request.Name.Trim(),
                BasePrice = new Money(request.BasePrice.Amount, request.BasePrice.Currency),
                MaxCapacity = unitType.MaxCapacity,
                CustomFeatures = request.CustomFeatures.Trim(),
                PricingMethod = request.PricingMethod,
                IsAvailable = true,
                CreatedBy = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _unitRepository.CreateUnitAsync(unit, cancellationToken);

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "CreateUnit",
                $"تم إنشاء وحدة جديدة {created.Id} باسم {created.Name}",
                created.Id,
                "Unit",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل إنشاء الوحدة بنجاح: UnitId={UnitId}", created.Id);
            return ResultDto<Guid>.Succeeded(created.Id, "تم إنشاء الوحدة بنجاح");
        }
    }
} 