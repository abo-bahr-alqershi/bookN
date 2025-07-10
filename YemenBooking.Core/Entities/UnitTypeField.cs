using System;
using System.Collections.Generic;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان حقول نوع العقار
/// UnitTypeField entity representing dynamic fields for a property type
/// </summary>
public class UnitTypeField : BaseEntity
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid UnitTypeId { get; set; }

    /// <summary>
    /// معرف نوع الحقل
    /// Field type identifier
    /// </summary>
    public Guid FieldTypeId { get; set; }

    /// <summary>
    /// اسم الحقل
    /// Field name
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// الاسم المعروض للحقل
    /// Display name of the field
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// وصف الحقل
    /// Field description
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// خيارات الحقل في حالة select أو multi_select (JSON)
    /// Field options in case of select or multi-select
    /// </summary>
    public string FieldOptions { get; set; }

    /// <summary>
    /// قواعد التحقق المخصصة (JSON)
    /// Custom validation rules
    /// </summary>
    public string ValidationRules { get; set; }

    /// <summary>
    /// هل الحقل إلزامي
    /// Is required
    /// </summary>
    public bool IsRequired { get; set; }

    /// <summary>
    /// هل يظهر في الفلترة
    /// Is searchable
    /// </summary>
    public bool IsSearchable { get; set; }

    /// <summary>
    /// هل يظهر للعملاء
    /// Is public
    /// </summary>
    public bool IsPublic { get; set; }

    /// <summary>
    /// ترتيب الحقل
    /// Sort order of the field
    /// </summary>
    public int SortOrder { get; set; }

    /// <summary>
    /// فئة الحقل (basic, amenities, location, pricing)
    /// Category of the field
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// نوع الوحدة المرتبط
    /// Unit type associated
    /// </summary>
    public virtual UnitType UnitType { get; set; }

    /// <summary>
    /// نوع الحقل المرتبط
    /// Field type associated
    /// </summary>
    public virtual FieldType FieldType { get; set; }

    /// <summary>
    /// قيم الحقل للوحدات
    /// Field values for units
    /// </summary>
    public virtual ICollection<UnitFieldValue> UnitFieldValues { get; set; } = new List<UnitFieldValue>();

    /// <summary>
    /// انضمامات مجموعات الحقول
    /// Field group links
    /// </summary>
    public virtual ICollection<FieldGroupField> FieldGroupFields { get; set; } = new List<FieldGroupField>();

    /// <summary>
    /// الفلاتر المرتبطة بهذا الحقل
    /// Search filters associated with this field
    /// </summary>
    public virtual ICollection<SearchFilter> SearchFilters { get; set; } = new List<SearchFilter>();

    /// <summary>
    /// يحدد ما إذا كان الحقل مخصصاً للوحدات
    /// Indicates if the field applies to units
    /// </summary>
    public bool IsForUnits { get; set; }
} 