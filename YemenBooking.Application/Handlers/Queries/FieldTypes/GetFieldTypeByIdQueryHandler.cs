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
    /// معالج استعلام جلب نوع الحقل حسب المعرف
    /// Query handler for GetFieldTypeByIdQuery
    /// </summary>
    public class GetFieldTypeByIdQueryHandler : IRequestHandler<GetFieldTypeByIdQuery, FieldTypeDto>
    {
        private readonly IFieldTypeRepository _fieldTypeRepository;
        private readonly ILogger<GetFieldTypeByIdQueryHandler> _logger;

        public GetFieldTypeByIdQueryHandler(
            IFieldTypeRepository fieldTypeRepository,
            ILogger<GetFieldTypeByIdQueryHandler> logger)
        {
            _fieldTypeRepository = fieldTypeRepository;
            _logger = logger;
        }

        public async Task<FieldTypeDto> Handle(GetFieldTypeByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام نوع الحقل حسب المعرف: {FieldTypeId}", request.FieldTypeId);

            if (request.FieldTypeId == Guid.Empty)
                throw new ValidationException(nameof(request.FieldTypeId), "معرف نوع الحقل غير صالح");

            var ft = await _fieldTypeRepository.GetQueryable()
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == request.FieldTypeId, cancellationToken);

            if (ft == null)
            {
                throw new NotFoundException("FieldType", $"$(request.FieldTypeId)", $"نوع الحقل بالمعرف {request.FieldTypeId} غير موجود");
            }

            return new FieldTypeDto
            {
                FieldTypeId = ft.Id.ToString(),
                Name = ft.Name,
                DisplayName = ft.DisplayName,
                ValidationRules = JsonSerializer.Deserialize<Dictionary<string, object>>(ft.ValidationRules) ?? new Dictionary<string, object>(),
                IsActive = ft.IsActive
            };
        }
    }
} 