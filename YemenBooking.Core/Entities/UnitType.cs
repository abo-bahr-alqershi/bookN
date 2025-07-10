namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان نوع الوحدة
/// Unit Type entity
/// </summary>
public class UnitType : BaseEntity
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid PropertyTypeId { get; set; }

    /// <summary>
    /// اسم نوع الوحدة (غرفة مزدوجة، جناح، شاليه كامل، فيلا)
    /// Unit type name (Double Room, Suite, Full Chalet, Villa)
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// الحد الأقصى للسعة
    /// Maximum capacity
    /// </summary>
    public int MaxCapacity { get; set; }

    /// <summary>
    /// نوع العقار المرتبط
    /// Property type associated
    /// </summary>
    public virtual PropertyType PropertyType { get; set; }

    /// <summary>
    /// الوحدات المرتبطة بهذا النوع
    /// Units associated with this type
    /// </summary>
    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    
    /// <summary>
    /// الحقول الديناميكية لنوع الوحدة
    /// Dynamic fields associated with this unit type
    /// </summary>
    public virtual ICollection<UnitTypeField> UnitTypeFields { get; set; } = new List<UnitTypeField>();

    /// <summary>
    /// مجموعات الحقول لنوع الوحدة
    /// Field groups associated with this unit type
    /// </summary>
    public virtual ICollection<FieldGroup> FieldGroups { get; set; } = new List<FieldGroup>();

    /// <summary>
    /// الفلاتر المخصصة لنوع الوحدة
    /// Search filters associated with this unit type fields
    /// </summary>
    public virtual ICollection<SearchFilter> SearchFilters { get; set; } = new List<SearchFilter>();

} 