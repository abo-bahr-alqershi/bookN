using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Reviews;

/// <summary>
/// استعلام للحصول على إحصائيات تقييم العقار
/// Query to get property rating statistics
/// </summary>
public class GetPropertyRatingStatsQuery : IRequest<ResultDto<PropertyRatingStatsDto>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
}