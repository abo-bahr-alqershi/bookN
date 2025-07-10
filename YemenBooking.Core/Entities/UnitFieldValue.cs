using System;
using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان قيم الحقول للوحدات
/// UnitFieldValue entity representing custom field values on units
/// </summary>
public class UnitFieldValue : BaseEntity, IFieldValue
{
    /// <summary>
    /// معرف الوحدة
    /// Unit identifier
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// معرف حقل نوع الوحدة
    /// Property type field identifier
    /// </summary>
    public Guid UnitTypeFieldId { get; set; }

    /// <summary>
    /// قيمة الحقل
    /// Field value
    /// </summary>
    public string FieldValue { get; set; } = string.Empty;

    /// <summary>
    /// الوحدة المرتبطة
    /// Unit associated
    /// </summary>
    public virtual Unit? Unit { get; set; }

    /// <summary>
    /// الحقل المرتبط
    /// Property type field associated
    /// </summary>
    public virtual UnitTypeField? UnitTypeField { get; set; }
} 