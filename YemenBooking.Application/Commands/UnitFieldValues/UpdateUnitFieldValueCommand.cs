namespace YemenBooking.Application.Commands.UnitFieldValues;

using System;
using MediatR;
using YemenBooking.Application.DTOs;
using Unit = MediatR.Unit;

/// <summary>
/// تحديث قيمة حقل للوحدة
/// Update unit field value
/// </summary>
public class UpdateUnitFieldValueCommand : IRequest<ResultDto<Unit>>
{
    /// <summary>
    /// معرف القيمة
    /// ValueId
    /// </summary>
    public Guid ValueId { get; set; }

    /// <summary>
    /// القيمة الجديدة للحقل
    /// New field value
    /// </summary>
    public string? NewFieldValue { get; set; }
} 