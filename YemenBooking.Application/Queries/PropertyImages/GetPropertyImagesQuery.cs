using System;
using System.Collections.Generic;
using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.PropertyImages;

/// <summary>
/// استعلام للحصول على جميع الصور الخاصة بعقار محدد
/// Query to get all images for a specific property
/// </summary>
public class GetPropertyImagesQuery : IRequest<ResultDto<IEnumerable<PropertyImageDto>>>
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
} 