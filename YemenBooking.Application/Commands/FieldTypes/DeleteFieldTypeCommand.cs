namespace YemenBooking.Application.Commands.FieldTypes;

using MediatR;
using YemenBooking.Application.DTOs;
using Unit = MediatR.Unit;

/// <summary>
/// حذف نوع الحقل
/// Delete field type
/// </summary>
public class DeleteFieldTypeCommand : IRequest<ResultDto<Unit>>
{
    /// <summary>
    /// معرف نوع الحقل
    /// FieldTypeId
    /// </summary>
    public Guid FieldTypeId { get; set; }
} 