using System;
using System.Collections.Generic;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان أنواع الحقول
/// FieldType entity representing available field types (text, number, boolean, etc.)
/// </summary>
public class FieldType : BaseEntity
{
    /// <summary>
    /// اسم نوع الحقل (text, number, boolean, date, select, multi_select, file, textarea)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// الاسم المعروض لنوع الحقل
    /// Display name of the field type
    /// </summary>
    public string DisplayName { get; set; }
    
    /// <summary>
    /// قواعد التحقق المتاحة لهذا النوع (JSON)
    /// Available validation rules for this field type
    /// </summary>
    public string ValidationRules { get; set; }

    /// <summary>
    /// الحقول المعرفة لنوع العقار المرتبطة بهذا النوع
    /// Property type fields associated with this field type
    /// </summary>
    public virtual ICollection<UnitTypeField> UnitTypeFields { get; set; } = new List<UnitTypeField>();
} 