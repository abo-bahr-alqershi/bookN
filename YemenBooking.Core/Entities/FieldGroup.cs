using System;
using System.Collections.Generic;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان مجموعات الحقول
/// FieldGroup entity representing grouping of fields per property type
/// </summary>
public class FieldGroup : BaseEntity
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid UnitTypeId { get; set; }

    /// <summary>
    /// اسم المجموعة
    /// Group name
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// الاسم المعروض للمجموعة
    /// Display name of the group
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// وصف المجموعة
    /// Description of the group
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// ترتيب العرض
    /// Sort order of the group
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// هل يمكن طي المجموعة
    /// Is collapsible
    /// </summary>
    public bool IsCollapsible { get; set; }

    /// <summary>
    /// هل تكون المجموعة موسعة افتراضياً
    /// Is expanded by default
    /// </summary>
    public bool IsExpandedByDefault { get; set; }

    /// <summary>
    /// نوع الوحدة المرتبطة
    /// Unit type associated
    /// </summary>
    public virtual UnitType UnitType { get; set; }

    /// <summary>
    /// روابط الحقول ضمن هذه المجموعة
    /// Field group links
    /// </summary>
    public virtual ICollection<FieldGroupField> FieldGroupFields { get; set; } = new List<FieldGroupField>();
} 