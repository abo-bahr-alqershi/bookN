using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Amenities;

/// <summary>
/// استعلام للحصول على جميع المرافق
/// Query to get all amenities
/// </summary>
public class GetAllAmenitiesQuery : IRequest<PaginatedResult<AmenityDto>>
{
    /// <summary>
    /// رقم الصفحة
    /// Page number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// حجم الصفحة
    /// Page size
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// مصطلح البحث النصي (اختياري)
    /// Full-text search term (optional)
    /// </summary>
    public string? SearchTerm { get; set; }
} 