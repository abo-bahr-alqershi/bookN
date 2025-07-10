namespace YemenBooking.Application.Queries.FieldTypes;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب جميع أنواع الحقول مع إمكانية التصفية حسب الحالة
/// Get all field types with optional active filter
/// </summary>
public class GetAllFieldTypesQuery : IRequest<List<FieldTypeDto>>
{
    /// <summary>
    /// حالة التفعيل المراد تصفيتها، null للجميع
    /// Filter by IsActive, null for all
    /// </summary>
    public bool? IsActive { get; set; }
} 