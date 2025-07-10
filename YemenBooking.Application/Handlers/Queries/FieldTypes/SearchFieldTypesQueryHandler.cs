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
using YemenBooking.Application.Queries.FieldTypes;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.FieldTypes
{
    /// <summary>
    /// معالج استعلام البحث في أنواع الحقول
    /// Query handler for SearchFieldTypesQuery
    /// </summary>
    public class SearchFieldTypesQueryHandler : IRequestHandler<SearchFieldTypesQuery, List<FieldTypeDto>>
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly ILogger<SearchFieldTypesQueryHandler> _logger;

        public SearchFieldTypesQueryHandler(
            IFieldTypeRepository fieldTypeRepository,
            ILogger<SearchFieldTypesQueryHandler> logger)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _logger = logger;
        }

        public async Task<List<FieldTypeDto>> Handle(SearchFieldTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام البحث في أنواع الحقول: {SearchTerm}", request.SearchTerm);

            var query = _fieldTypeRepository.GetQueryable().AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                query = query.Where(ft =>
                    ft.Name.ToLower().Contains(term) ||
                    ft.DisplayName.ToLower().Contains(term));
            }

            if (request.IsActive.HasValue)
            {
                query = query.Where(ft => ft.IsActive == request.IsActive.Value);
            }

            var types = await query.ToListAsync(cancellationToken);

            return types.Select(ft => new FieldTypeDto
            {
                FieldTypeId = ft.Id.ToString(),
                Name = ft.Name,
                DisplayName = ft.DisplayName,
                ValidationRules = JsonSerializer.Deserialize<Dictionary<string, object>>(ft.ValidationRules) ?? new Dictionary<string, object>(),
                IsActive = ft.IsActive
            }).ToList();
        }
    }
} 