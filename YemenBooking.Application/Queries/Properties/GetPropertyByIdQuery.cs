using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Properties;

/// <summary>
/// استعلام للحصول على بيانات العقار بواسطة المعرف
/// Query to get property details by ID
/// </summary>
public class GetPropertyByIdQuery : IRequest<ResultDto<PropertyDetailsDto>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }
} 