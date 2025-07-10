using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Properties;

/// <summary>
/// استعلام للحصول على مرافق العقار
/// Query to get property amenities
/// </summary>
public class GetPropertyAmenitiesQuery : IRequest<ResultDto<IEnumerable<AmenityDto>>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 