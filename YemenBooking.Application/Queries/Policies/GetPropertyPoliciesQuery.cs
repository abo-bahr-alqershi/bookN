using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Policies;

/// <summary>
/// استعلام للحصول على سياسات العقار
/// Query to get property policies
/// </summary>
public class GetPropertyPoliciesQuery : IRequest<ResultDto<IEnumerable<PolicyDto>>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
}