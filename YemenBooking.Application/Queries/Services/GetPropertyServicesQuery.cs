using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Services;

/// <summary>
/// استعلام للحصول على خدمات العقار
/// Query to get property services
/// </summary>
public class GetPropertyServicesQuery : IRequest<ResultDto<IEnumerable<ServiceDto>>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 