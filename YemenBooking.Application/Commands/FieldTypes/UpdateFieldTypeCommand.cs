namespace YemenBooking.Application.Commands.FieldTypes;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;
using Unit = MediatR.Unit;

/// <summary>
/// تحديث نوع الحقل
/// Update existing field type
/// </summary>
public class UpdateFieldTypeCommand : IRequest<ResultDto<Unit>>
{
    /// <summary>
    /// معرف نوع الحقل
    /// FieldTypeId
    /// </summary>
    public Guid FieldTypeId { get; set; }

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
    public bool IsActive { get; set; }
} 