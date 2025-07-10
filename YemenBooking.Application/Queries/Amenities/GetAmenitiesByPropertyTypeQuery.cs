using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Amenities;

/// <summary>
/// استعلام للحصول على مرافق نوع العقار
/// Query to get amenities by property type
/// </summary>
public class GetAmenitiesByPropertyTypeQuery : IRequest<ResultDto<IEnumerable<AmenityDto>>>
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type ID
    /// </summary>
    public Guid PropertyTypeId { get; set; }
}