using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Commands.Properties;

/// <summary>
/// أمر لتحديث بيانات العقار
/// Command to update property information
/// </summary>
public class UpdatePropertyCommand : IRequest<ResultDto<bool>>
{
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// اسم العقار المحدث
    /// Updated property name
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// العنوان المحدث للعقار
    /// Updated property address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// وصف العقار المحدث
    /// Updated property description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// خط العرض المحدث للموقع الجغرافي
    /// Updated latitude for geographic location
    /// </summary>
    public double? Latitude { get; set; }

    /// <summary>
    /// خط الطول المحدث للموقع الجغرافي
    /// Updated longitude for geographic location
    /// </summary>
    public double? Longitude { get; set; }

    /// <summary>
    /// المدينة المحدثة
    /// Updated city
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    /// تقييم النجوم المحدث
    /// Updated star rating
    /// </summary>
    public int? StarRating { get; set; }
} 