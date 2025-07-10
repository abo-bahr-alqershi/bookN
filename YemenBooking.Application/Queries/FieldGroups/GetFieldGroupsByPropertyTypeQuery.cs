namespace YemenBooking.Application.Queries.FieldGroups;

using MediatR;
using System.Collections.Generic;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب مجموعات الحقول لنوع عقار معين
/// Get field groups by property type
/// </summary>
public class GetFieldGroupsByPropertyTypeQuery : IRequest<List<FieldGroupDto>>
{
    /// <summary>
    /// معرف نوع العقار
    /// PropertyTypeId
    /// </summary>
    public string PropertyTypeId { get; set; }
} 