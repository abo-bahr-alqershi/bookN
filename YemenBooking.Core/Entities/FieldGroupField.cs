using System;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان ربط الحقول والمجموعات
/// FieldGroupField entity representing link between fields and groups
/// </summary>
public class FieldGroupField : BaseEntity
{
    /// <summary>
    /// معرف الحقل
    /// Field identifier
    /// </summary>
    public Guid FieldId { get; set; }

    /// <summary>
    /// معرف المجموعة
    /// Group identifier
    /// </summary>
    public Guid GroupId { get; set; }

    /// <summary>
    /// ترتيب الحقل داخل المجموعة
    /// Sort order within the group
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// العلاقة مع الحقل
    /// Property type field associated
    /// </summary>
    public virtual UnitTypeField UnitTypeField { get; set; }

    /// <summary>
    /// العلاقة مع المجموعة
    /// Field group associated
    /// </summary>
    public virtual FieldGroup FieldGroup { get; set; }
} 