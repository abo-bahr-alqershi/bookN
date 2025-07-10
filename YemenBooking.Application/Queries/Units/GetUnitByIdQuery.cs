using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Units;

/// <summary>
/// استعلام للحصول على بيانات الوحدة بواسطة المعرف
/// Query to get unit details by ID
/// </summary>
public class GetUnitByIdQuery : IRequest<ResultDto<UnitDetailsDto>>
{
    /// <summary>
    /// معرف الوحدة
    /// Unit ID
    /// </summary>
    public Guid UnitId { get; set; }
} 