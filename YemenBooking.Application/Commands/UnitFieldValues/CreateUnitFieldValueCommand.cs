namespace YemenBooking.Application.Commands.UnitFieldValues;

using System;
using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// إنشاء قيمة حقل للوحدة
/// Create unit field value
/// </summary>
public class CreateUnitFieldValueCommand : IRequest<ResultDto<Guid>>
{
    /// <summary>
    /// معرف الوحدة
    /// UnitId
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// معرف الحقل
    /// FieldId
    /// </summary>
    public Guid FieldId { get; set; }

    /// <summary>
    /// قيمة الحقل
    /// FieldValue
    /// </summary>
    public string? FieldValue { get; set; }
} 