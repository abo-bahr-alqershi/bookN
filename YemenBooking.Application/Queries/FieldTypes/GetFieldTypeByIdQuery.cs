namespace YemenBooking.Application.Queries.FieldTypes;

using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب نوع الحقل حسب المعرف
/// Get field type by id
/// </summary>
public class GetFieldTypeByIdQuery : IRequest<FieldTypeDto>
{
    /// <summary>
    /// معرف نوع الحقل
    /// FieldTypeId
    /// </summary>
    public Guid FieldTypeId { get; set; }
} 