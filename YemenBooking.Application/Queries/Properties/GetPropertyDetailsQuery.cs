namespace YemenBooking.Application.Queries.Properties;

using System;
using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب تفاصيل العقار مع الحقول الديناميكية
/// Get property details including dynamic field values
/// </summary>
public class GetPropertyDetailsQuery : IRequest<ResultDto<PropertyDetailsDto>>
{
    /// <summary>
    /// معرف العقار
    /// PropertyId
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// تضمين الوحدات الفرعية (اختياري)
    /// IncludeUnits
    /// </summary>
    public bool IncludeUnits { get; set; } = true;
} 