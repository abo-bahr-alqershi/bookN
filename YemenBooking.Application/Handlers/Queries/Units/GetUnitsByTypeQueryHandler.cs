using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.Units;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Application.Handlers.Queries.Units
{
    /// <summary>
    /// معالج استعلام الحصول على الوحدات حسب النوع
    /// Query handler for GetUnitsByTypeQuery
    /// </summary>
    public class GetUnitsByTypeQueryHandler : IRequestHandler<GetUnitsByTypeQuery, PaginatedResult<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUnitsByTypeQueryHandler> _logger;

        public GetUnitsByTypeQueryHandler(
            IUnitRepository unitRepository,
            ICurrentUserService currentUserService,
            ILogger<GetUnitsByTypeQueryHandler> logger)
        {
            _unitRepository = unitRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<PaginatedResult<UnitDto>> Handle(GetUnitsByTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام الوحدات حسب النوع: {UnitTypeId}", request.UnitTypeId);

            var query = _unitRepository.GetQueryable()
                .AsNoTracking()
                .Include(u => u.Property)
                .Include(u => u.UnitType)
                .Include(u => u.FieldValues)
                .Where(u => u.UnitTypeId == request.UnitTypeId);

            var currentUser = await _currentUserService.GetCurrentUserAsync(cancellationToken);
            var role = _currentUserService.Role;
            bool isOwner = currentUser != null && _currentUserService.PropertyId.HasValue;
            if (role != "Admin" && !isOwner)
                query = query.Where(u => u.IsAvailable && u.Property.IsApproved);

            if (request.IsAvailable.HasValue)
                query = query.Where(u => u.IsAvailable == request.IsAvailable.Value);

            if (request.MinBasePrice.HasValue)
                query = query.Where(u => u.BasePrice.Amount >= request.MinBasePrice.Value);

            if (request.MaxBasePrice.HasValue)
                query = query.Where(u => u.BasePrice.Amount <= request.MaxBasePrice.Value);

            if (request.MinCapacity.HasValue)
                query = query.Where(u => u.MaxCapacity >= request.MinCapacity.Value);

            if (!string.IsNullOrWhiteSpace(request.NameContains))
                query = query.Where(u => u.Name.Contains(request.NameContains));

            var totalCount = await query.CountAsync(cancellationToken);

            var units = await query
                .OrderBy(u => u.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var dtos = units.Select(u => new UnitDto
            {
                Id = u.Id,
                PropertyId = u.PropertyId,
                UnitTypeId = u.UnitTypeId,
                Name = u.Name,
                BasePrice = new MoneyDto { Amount = u.BasePrice.Amount, Currency = u.BasePrice.Currency },
                CustomFeatures = u.CustomFeatures,
                IsAvailable = u.IsAvailable,
                PropertyName = u.Property.Name,
                UnitTypeName = u.UnitType.Name,
                PricingMethod = u.PricingMethod,
                FieldValues = u.FieldValues.Select(fv => new UnitFieldValueDto
                {
                    ValueId = fv.Id,
                    UnitId = fv.UnitId,
                    FieldId = fv.UnitTypeFieldId,
                    FieldName = fv.UnitTypeField.FieldName,
                    DisplayName = fv.UnitTypeField.DisplayName,
                    FieldValue = fv.FieldValue,
                    Field = new UnitTypeFieldDto
                    {
                        FieldId = fv.UnitTypeField.Id.ToString(),
                        PropertyTypeId = fv.UnitTypeField.UnitTypeId.ToString(),
                        FieldTypeId = fv.UnitTypeField.FieldTypeId.ToString(),
                        FieldName = fv.UnitTypeField.FieldName,
                        DisplayName = fv.UnitTypeField.DisplayName,
                        Description = fv.UnitTypeField.Description,
                        FieldOptions = JsonSerializer.Deserialize<Dictionary<string, object>>(fv.UnitTypeField.FieldOptions) ?? new Dictionary<string, object>(),
                        ValidationRules = JsonSerializer.Deserialize<Dictionary<string, object>>(fv.UnitTypeField.ValidationRules) ?? new Dictionary<string, object>(),
                        IsRequired = fv.UnitTypeField.IsRequired,
                        IsSearchable = fv.UnitTypeField.IsSearchable,
                        IsPublic = fv.UnitTypeField.IsPublic,
                        SortOrder = fv.UnitTypeField.SortOrder,
                        Category = fv.UnitTypeField.Category,
                        GroupId = fv.UnitTypeField.FieldGroupFields.FirstOrDefault()?.GroupId.ToString() ?? string.Empty
                    },
                    CreatedAt = fv.CreatedAt,
                    UpdatedAt = fv.UpdatedAt
                }).ToList()
            }).ToList();

            return PaginatedResult<UnitDto>.Create(dtos, request.PageNumber, request.PageSize, totalCount);
        }
    }
} 