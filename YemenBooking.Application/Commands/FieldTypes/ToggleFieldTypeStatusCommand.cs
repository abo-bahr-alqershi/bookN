namespace YemenBooking.Application.Commands.FieldTypes;

using MediatR;
using YemenBooking.Application.DTOs;
using Unit = MediatR.Unit;

/// <summary>
/// تغيير حالة التفعيل لنوع الحقل
/// Toggle field type active status
/// </summary>
public class ToggleFieldTypeStatusCommand : IRequest<ResultDto<bool>>
{
    /// <summary>
    /// معرف نوع الحقل
    /// FieldTypeId
    /// </summary>
    public Guid FieldTypeId { get; set; }

    /// <summary>
    /// الحالة الجديدة
    /// IsActive
    /// </summary>
    public bool IsActive { get; set; }
} 