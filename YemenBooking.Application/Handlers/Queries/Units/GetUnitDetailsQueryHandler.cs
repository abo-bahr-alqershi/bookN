using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.Units;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using System.Collections.Generic;

namespace YemenBooking.Application.Handlers.Queries.Units
{
    /// <summary>
    /// معالج استعلام جلب تفاصيل الوحدة مع الحقول الديناميكية
    /// Query handler for GetUnitDetailsQuery
    /// </summary>
    public class GetUnitDetailsQueryHandler : IRequestHandler<GetUnitDetailsQuery, ResultDto<UnitDetailsDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUnitDetailsQueryHandler> _logger;

        public GetUnitDetailsQueryHandler(
            IUnitRepository unitRepository,
            ICurrentUserService currentUserService,
            ILogger<GetUnitDetailsQueryHandler> logger)
        {
            _unitRepository = unitRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ResultDto<UnitDetailsDto>> Handle(GetUnitDetailsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام تفاصيل الوحدة: {UnitId}", request.UnitId);

            var unit = await _unitRepository.GetQueryable()
                .AsNoTracking()
                .Include(u => u.Property)
                .Include(u => u.UnitType)
                .Include(u => u.FieldValues)
                    .ThenInclude(fv => fv.UnitTypeField)
                .FirstOrDefaultAsync(u => u.Id == request.UnitId, cancellationToken);

            if (unit == null)
                return ResultDto<UnitDetailsDto>.Failure($"الوحدة بالمعرف {request.UnitId} غير موجود");

            var currentUser = await _currentUserService.GetCurrentUserAsync(cancellationToken);
            var role = _currentUserService.Role;
            bool isOwner = currentUser != null && unit.Property.OwnerId == _currentUserService.UserId;
            if (role != "Admin" && !isOwner)
            {
                if (!unit.Property.IsApproved || !unit.IsAvailable)
                {
                    return ResultDto<UnitDetailsDto>.Failure("ليس لديك صلاحية لعرض هذه الوحدة");
                }
            }

            // تم تعطيل تجميع الحقول الديناميكية مؤقتاً لتجنب أخطاء التوثيق
            var fieldGroups = new List<FieldGroupWithValuesDto>();

            var dto = new UnitDetailsDto
            {
                Id = unit.Id,
                PropertyId = unit.PropertyId,
                UnitTypeId = unit.UnitTypeId,
                Name = unit.Name,
                BasePrice = new MoneyDto { Amount = unit.BasePrice.Amount, Currency = unit.BasePrice.Currency },
                CustomFeatures = unit.CustomFeatures,
                IsAvailable = unit.IsAvailable,
                PropertyName = unit.Property.Name,
                UnitTypeName = unit.UnitType.Name,
                PricingMethod = unit.PricingMethod,
                FieldValues = unit.FieldValues.Select(fv => new UnitFieldValueDto
                {
                    FieldId = fv.UnitTypeFieldId,
                    FieldValue = fv.FieldValue
                }).ToList(),
                DynamicFields = request.IncludeDynamicFields ? fieldGroups : new List<FieldGroupWithValuesDto>()
            };

            _logger.LogInformation("تم جلب تفاصيل الوحدة بنجاح: {UnitId}", request.UnitId);
            return ResultDto<UnitDetailsDto>.Succeeded(dto);
        }
    }
} 