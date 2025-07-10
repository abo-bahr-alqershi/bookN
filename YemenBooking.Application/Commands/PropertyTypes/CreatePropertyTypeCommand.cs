using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Commands.PropertyTypes;

/// <summary>
/// أمر لإنشاء نوع عقار جديد
/// Command to create a new property type
/// </summary>
public class CreatePropertyTypeCommand : IRequest<ResultDto<Guid>>
{
    /// <summary>
    /// اسم نوع العقار
    /// Property type name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// وصف نوع العقار
    /// Property type description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// المرافق الافتراضية لنوع العقار (JSON)
    /// Default amenities for the property type (JSON)
    /// </summary>
    public string DefaultAmenities { get; set; } = string.Empty;
} 