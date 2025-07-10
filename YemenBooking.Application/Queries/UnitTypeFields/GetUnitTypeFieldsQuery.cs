namespace YemenBooking.Application.Queries.UnitTypeFields;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب جميع الحقول الديناميكية لنوع العقار
/// Get dynamic fields for property type
/// </summary>
public class GetUnitTypeFieldsQuery : IRequest<List<UnitTypeFieldDto>>
{
    /// <summary>
    /// معرف نوع العقار
    /// PropertyTypeId
    /// </summary>
    public string PropertyTypeId { get; set; }

    /// <summary>
    /// حالة التفعيل (اختياري)
    /// IsActive
    /// </summary>
    public bool? IsActive { get; set; }

    /// <summary>
    /// يظهر في البحث فقط (اختياري)
    /// IsSearchable
    /// </summary>
    public bool? IsSearchable { get; set; }

    /// <summary>
    /// عام فقط (اختياري)
    /// IsPublic
    /// </summary>
    public bool? IsPublic { get; set; }

    /// <summary>
    /// يحدد ما إذا كان الحقل مخصص للوحدات للتصفية (اختياري)
    /// Filter for unit-specific fields
    /// </summary>
    public bool? IsForUnits { get; set; }

    /// <summary>
    /// فئة الحقل (اختياري)
    /// Category
    /// </summary>
    public string Category { get; set; }
} 