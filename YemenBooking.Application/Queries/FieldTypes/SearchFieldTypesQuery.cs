namespace YemenBooking.Application.Queries.FieldTypes;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;

/// <summary>
/// بحث في أنواع الحقول حسب نص البحث والحالة
/// Search field types by term and optional active filter
/// </summary>
public class SearchFieldTypesQuery : IRequest<List<FieldTypeDto>>
{
    /// <summary>
    /// نص البحث
    /// Search term
    /// </summary>
    public string SearchTerm { get; set; }

    /// <summary>
    /// حالة التفعيل المراد تصفيتها، null للجميع
    /// Filter by IsActive, null for all
    /// </summary>
    public bool? IsActive { get; set; }
} 