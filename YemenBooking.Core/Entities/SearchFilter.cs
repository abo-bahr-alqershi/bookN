using System;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان فلاتر البحث
/// SearchFilter entity representing filters for dynamic fields
/// </summary>
public class SearchFilter : BaseEntity
{
    /// <summary>
    /// معرف الحقل المرتبط
    /// Field identifier
    /// </summary>
    public Guid FieldId { get; set; }

    /// <summary>
    /// نوع الفلتر (range, exact, contains, boolean, select)
    /// Filter type
    /// </summary>
    public string FilterType { get; set; }

    /// <summary>
    /// الاسم المعروض للفلتر
    /// Display name of the filter
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// خيارات إضافية للفلتر (JSON)
    /// Filter options
    /// </summary>
    public string FilterOptions { get; set; }

    /// <summary>
    /// حالة تفعيل الفلتر
    /// Is active
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// ترتيب عرض الفلتر
    /// Sort order
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// علاقة الحقل المرتبط
    /// Property type field associated with this filter
    /// </summary>
    public virtual UnitTypeField UnitTypeField { get; set; }
} 