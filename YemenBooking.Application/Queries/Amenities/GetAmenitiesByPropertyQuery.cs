using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Amenities;

/// <summary>
/// استعلام للحصول على مرافق العقار
/// Query to get amenities by property
/// </summary>
public class GetAmenitiesByPropertyQuery : IRequest<ResultDto<IEnumerable<AmenityDto>>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 