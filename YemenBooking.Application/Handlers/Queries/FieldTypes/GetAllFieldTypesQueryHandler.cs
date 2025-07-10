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
using YemenBooking.Application.Queries.FieldTypes;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.FieldTypes
{
    /// <summary>
    /// معالج استعلام جلب جميع أنواع الحقول مع إمكانية التصفية بالحالة
    /// Query handler for GetAllFieldTypesQuery
    /// </summary>
    public class GetAllFieldTypesQueryHandler : IRequestHandler<GetAllFieldTypesQuery, List<FieldTypeDto>>
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly ILogger<GetAllFieldTypesQueryHandler> _logger;

        public GetAllFieldTypesQueryHandler(
            IFieldTypeRepository fieldTypeRepository,
            ILogger<GetAllFieldTypesQueryHandler> logger)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _logger = logger;
        }

        public async Task<List<FieldTypeDto>> Handle(GetAllFieldTypesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام جميع أنواع الحقول");

            var query = _fieldTypeRepository.GetQueryable().AsNoTracking();

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