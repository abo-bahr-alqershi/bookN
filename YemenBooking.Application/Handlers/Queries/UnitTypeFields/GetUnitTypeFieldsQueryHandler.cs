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
using YemenBooking.Application.Exceptions;
using YemenBooking.Application.Queries.UnitTypeFields;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.UnitTypeFields
{
    /// <summary>
    /// معالج استعلام جلب جميع الحقول الديناميكية لنوع العقار
    /// Query handler for GetUnitTypeFieldsQuery
    /// </summary>
    public class GetUnitTypeFieldsQueryHandler : IRequestHandler<GetUnitTypeFieldsQuery, List<UnitTypeFieldDto>>
    {
        private readonly IUnitTypeFieldRepository _fieldRepo;
        private readonly ILogger<GetUnitTypeFieldsQueryHandler> _logger;

        public GetUnitTypeFieldsQueryHandler(
            IUnitTypeFieldRepository fieldRepo,
            ILogger<GetUnitTypeFieldsQueryHandler> logger)
        {
            _fieldRepo = fieldRepo;
            _logger = logger;
        }

        public async Task<List<UnitTypeFieldDto>> Handle(GetUnitTypeFieldsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام الحقول الديناميكية لنوع العقار: {PropertyTypeId}", request.PropertyTypeId);

            if (!Guid.TryParse(request.PropertyTypeId, out var typeId))
                throw new ValidationException(nameof(request.PropertyTypeId), "معرف نوع العقار غير صالح");

            var query = _fieldRepo.GetQueryable().AsNoTracking()
                .Where(f => f.UnitTypeId == typeId)
                .Where(f => !request.IsActive.HasValue || f.IsActive == request.IsActive.Value)
                .Where(f => !request.IsSearchable.HasValue || f.IsSearchable == request.IsSearchable.Value)
                .Where(f => !request.IsPublic.HasValue || f.IsPublic == request.IsPublic.Value);

            if (!string.IsNullOrWhiteSpace(request.Category))
                query = query.Where(f => f.Category == request.Category);

            if (request.IsForUnits.HasValue)
                query = query.Where(f => f.IsForUnits == request.IsForUnits.Value);

            var entities = await query.OrderBy(f => f.SortOrder).ToListAsync(cancellationToken);

            return entities.Select(f => new UnitTypeFieldDto
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
                GroupId = f.FieldGroupFields.FirstOrDefault()?.GroupId.ToString() ?? string.Empty,
                IsForUnits = f.IsForUnits
            }).ToList();
        }
    }
} 