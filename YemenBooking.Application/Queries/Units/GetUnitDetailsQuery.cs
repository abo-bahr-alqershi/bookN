namespace YemenBooking.Application.Queries.Units;

using System;
using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب تفاصيل الوحدة مع الحقول الديناميكية
/// Get unit details including dynamic field values
/// </summary>
public class GetUnitDetailsQuery : IRequest<ResultDto<UnitDetailsDto>>
{
    /// <summary>
    /// معرف الوحدة
    /// UnitId
    /// </summary>
    public Guid UnitId { get; set; }

    /// <summary>
    /// تضمين القيم الديناميكية (اختياري)
    /// IncludeDynamicFields
    /// </summary>
    public bool IncludeDynamicFields { get; set; } = true;
} 