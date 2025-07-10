using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Exceptions;
using YemenBooking.Application.Queries.Amenities;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.Amenities
{
    /// <summary>
    /// معالج استعلام الحصول على مرافق العقار
    /// Query handler for GetAmenitiesByPropertyQuery
    /// </summary>
    public class GetAmenitiesByPropertyQueryHandler : IRequestHandler<GetAmenitiesByPropertyQuery, ResultDto<IEnumerable<AmenityDto>>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly ILogger<GetAmenitiesByPropertyQueryHandler> _logger;

        public GetAmenitiesByPropertyQueryHandler(
            IAmenityRepository amenityRepository,
            ILogger<GetAmenitiesByPropertyQueryHandler> logger)
        {
            _amenityRepository = amenityRepository;
            _logger = logger;
        }

        public async Task<ResultDto<IEnumerable<AmenityDto>>> Handle(GetAmenitiesByPropertyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام مرافق العقار: {PropertyId}", request.PropertyId);

            if (request.PropertyId == Guid.Empty)
                throw new ValidationException(nameof(request.PropertyId), "معرف العقار غير صالح");

            var amenities = await _amenityRepository.GetAmenitiesByPropertyAsync(request.PropertyId, cancellationToken);

            var dtos = amenities.Select(pa => new AmenityDto
            {
                Id = pa.PropertyTypeAmenity.Amenity.Id,
                Name = pa.PropertyTypeAmenity.Amenity.Name,
                Description = pa.PropertyTypeAmenity.Amenity.Description
            }).ToList();

            return ResultDto<IEnumerable<AmenityDto>>.Ok(dtos, "تم جلب مرافق العقار بنجاح");
        }
    }
} 