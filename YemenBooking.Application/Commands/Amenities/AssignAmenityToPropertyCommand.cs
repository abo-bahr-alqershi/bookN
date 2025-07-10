using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Commands.Amenities;

/// <summary>
/// أمر لتخصيص مرفق لعقار
/// Command to assign an amenity to a property
/// </summary>
public class AssignAmenityToPropertyCommand : IRequest<ResultDto<bool>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// معرف المرفق
    /// Amenity ID
    /// </summary>
    public Guid AmenityId { get; set; }
} 