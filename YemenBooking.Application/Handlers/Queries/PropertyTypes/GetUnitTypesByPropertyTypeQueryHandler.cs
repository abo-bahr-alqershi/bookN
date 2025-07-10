using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.PropertyTypes;
using YemenBooking.Application.Exceptions;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.PropertyTypes
{
    /// <summary>
    /// معالج استعلام الحصول على أنواع الوحدات لنوع عقار معين
    /// Query handler for GetUnitTypesByPropertyTypeQuery
    /// </summary>
    public class GetUnitTypesByPropertyTypeQueryHandler : IRequestHandler<GetUnitTypesByPropertyTypeQuery, PaginatedResult<UnitTypeDto>>
    {
        private readonly IUnitTypeRepository _repo;
        private readonly ILogger<GetUnitTypesByPropertyTypeQueryHandler> _logger;

        public GetUnitTypesByPropertyTypeQueryHandler(
            IUnitTypeRepository repo,
            ILogger<GetUnitTypesByPropertyTypeQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<PaginatedResult<UnitTypeDto>> Handle(GetUnitTypesByPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام أنواع الوحدات لنوع العقار: {PropertyTypeId}", request.PropertyTypeId);

            if (request.PropertyTypeId == Guid.Empty)
                throw new ValidationException(nameof(request.PropertyTypeId), "معرف نوع العقار غير صالح");

            var unitTypes = await _repo.GetUnitTypesByPropertyTypeAsync(request.PropertyTypeId, cancellationToken);

            var dtos = unitTypes.Select(ut => new UnitTypeDto
            {
                Id = ut.Id,
                PropertyTypeId = ut.PropertyTypeId,
                Name = ut.Name,
                Description = string.Empty,
                DefaultPricingRules = string.Empty
            }).ToList();

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