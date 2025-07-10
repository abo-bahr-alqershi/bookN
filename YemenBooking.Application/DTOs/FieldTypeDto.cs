namespace YemenBooking.Application.DTOs;

using System.Collections.Generic;

/// <summary>
/// بيانات نقل نوع الحقل
/// DTO for FieldType entity
/// </summary>
public class FieldTypeDto
{
    /// <summary>
    /// معرف نوع الحقل
    /// FieldTypeId
    /// </summary>
    public string FieldTypeId { get; set; }

    /// <summary>
    /// اسم نوع الحقل
    /// Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// الاسم المعروض لنوع الحقل
    /// DisplayName
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    /// قواعد التحقق
    /// ValidationRules
    /// </summary>
    public Dictionary<string, object> ValidationRules { get; set; }

    /// <summary>
    /// حالة التفعيل
    /// IsActive
    /// </summary>
    public bool IsActive { get; set; }
} 