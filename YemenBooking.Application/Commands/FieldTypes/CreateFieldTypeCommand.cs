namespace YemenBooking.Application.Commands.FieldTypes;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;

/// <summary>
/// إنشاء نوع حقل جديد (للمسؤول فقط)
/// Create new field type (Admin only)
/// </summary>
public class CreateFieldTypeCommand : IRequest<ResultDto<string>>
{
    /// <summary>
    /// اسم نوع الحقل
    /// Name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// الاسم المعروض لنوع الحقل
    /// DisplayName
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// قواعد التحقق
    /// ValidationRules
    /// </summary>
    public Dictionary<string, object> ValidationRules { get; set; } = new();

    /// <summary>
    /// حالة التفعيل
    /// IsActive
    /// </summary>
    public bool IsActive { get; set; } = true;
} 