using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.PropertyTypes;
using YemenBooking.Application.Exceptions;
using YemenBooking.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace YemenBooking.Application.Handlers.Queries.PropertyTypes
{
    /// <summary>
    /// معالج استعلام الحصول على أنواع الوحدات لنوع عقار معين
    /// Query handler for GetUnitTypesByPropertyTypeQuery
    /// </summary>
    public class GetUnitTypesByPropertyTypeQueryHandler : IRequestHandler<GetUnitTypesByPropertyTypeQuery, PaginatedResult<UnitTypeDto>>
    {
        private readonly IUnitTypeRepository _repo;
        private readonly IUnitTypeFieldRepository _fieldRepo;
        private readonly ISearchFilterRepository _filterRepo;
        private readonly ILogger<GetUnitTypesByPropertyTypeQueryHandler> _logger;

        public GetUnitTypesByPropertyTypeQueryHandler(
            IUnitTypeRepository repo,
            IUnitTypeFieldRepository fieldRepo,
            ISearchFilterRepository filterRepo,
            ILogger<GetUnitTypesByPropertyTypeQueryHandler> logger)
        {
            _repo = repo;
            _fieldRepo = fieldRepo;
            _filterRepo = filterRepo;
            _logger = logger;
        }

        public async Task<PaginatedResult<UnitTypeDto>> Handle(GetUnitTypesByPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام أنواع الوحدات لنوع العقار: {PropertyTypeId}", request.PropertyTypeId);

            if (request.PropertyTypeId == Guid.Empty)
                throw new ValidationException(nameof(request.PropertyTypeId), "معرف نوع العقار غير صالح");

            var unitTypes = await _repo.GetUnitTypesByPropertyTypeAsync(request.PropertyTypeId, cancellationToken);

            var dtos = new List<UnitTypeDto>();
            foreach (var ut in unitTypes)
            {
                var dto = new UnitTypeDto
                {
                    Id = ut.Id,
                    PropertyTypeId = ut.PropertyTypeId,
                    Name = ut.Name,
                    Description = ut.Description,
                    DefaultPricingRules = ut.DefaultPricingRules
                };

                // جلب الحقول الديناميكية
                var fields = await _fieldRepo.GetFieldsByUnitTypeIdAsync(ut.Id, cancellationToken);
                dto.Fields = fields.Select(f => new UnitTypeFieldDto
                {
                    FieldId = f.Id.ToString(),
                    PropertyTypeId = f.UnitTypeId.ToString(),
                    FieldTypeId = f.FieldTypeId.ToString(),
                    FieldName = f.FieldName,
                    DisplayName = f.DisplayName,
                    Description = f.Description,
                    FieldOptions = JsonSerializer.Deserialize<Dictionary<string, object>>(f.FieldOptions) ?? new Dictionary<string, object>(),
                    ValidationRules = JsonSerializer.Deserialize<Dictionary<string, object>>(f.ValidationRules) ?? new Dictionary<string, object>(),
                    IsRequired = f.IsRequired,
                    IsSearchable = f.IsSearchable,
                    IsPublic = f.IsPublic,
                    SortOrder = f.SortOrder,
                    Category = f.Category,
                    GroupId = f.FieldGroupFields.FirstOrDefault()?.GroupId.ToString() ?? string.Empty
                }).ToList();

                // جلب الفلاتر الديناميكية المتعلقة بالحقول
                var filters = await _filterRepo.GetQueryable()
                    .AsNoTracking()
                    .Include(sf => sf.UnitTypeField)
                    .Where(sf => sf.UnitTypeField.UnitTypeId == ut.Id && sf.IsActive)
                    .OrderBy(sf => sf.SortOrder)
                    .ToListAsync(cancellationToken);
                dto.Filters = filters.Select(sf => new SearchFilterDto
                {
                    FilterId = sf.Id,
                    FieldId = sf.FieldId,
                    FilterType = sf.FilterType,
                    DisplayName = sf.DisplayName,
                    FilterOptions = JsonSerializer.Deserialize<Dictionary<string, object>>(sf.FilterOptions) ?? new Dictionary<string, object>(),
                    IsActive = sf.IsActive,
                    SortOrder = sf.SortOrder,
                    Field = new UnitTypeFieldDto
                    {
                        FieldId = sf.UnitTypeField.Id.ToString(),
                        PropertyTypeId = sf.UnitTypeField.UnitTypeId.ToString(),
                        FieldTypeId = sf.UnitTypeField.FieldTypeId.ToString(),
                        FieldName = sf.UnitTypeField.FieldName,
                        DisplayName = sf.UnitTypeField.DisplayName,
                        Description = sf.UnitTypeField.Description,
                        FieldOptions = JsonSerializer.Deserialize<Dictionary<string, object>>(sf.UnitTypeField.FieldOptions) ?? new Dictionary<string, object>(),
                        ValidationRules = JsonSerializer.Deserialize<Dictionary<string, object>>(sf.UnitTypeField.ValidationRules) ?? new Dictionary<string, object>(),
                        IsRequired = sf.UnitTypeField.IsRequired,
                        IsSearchable = sf.UnitTypeField.IsSearchable,
                        IsPublic = sf.UnitTypeField.IsPublic,
                        SortOrder = sf.UnitTypeField.SortOrder,
                        Category = sf.UnitTypeField.Category,
                        GroupId = sf.UnitTypeField.FieldGroupFields.FirstOrDefault()?.GroupId.ToString() ?? string.Empty
                    }
                }).ToList();

                dtos.Add(dto);
            }

            var totalCount = dtos.Count;
            var items = dtos.Skip((request.PageNumber - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .ToList();

            return new PaginatedResult<UnitTypeDto>
            {
                Items = items,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
} 