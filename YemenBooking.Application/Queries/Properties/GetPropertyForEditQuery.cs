namespace YemenBooking.Application.Queries.Properties;

using System;
using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب بيانات العقار للتحرير
/// Get property data for edit form
/// </summary>
public class GetPropertyForEditQuery : IRequest<ResultDto<PropertyEditDto>>
{
    /// <summary>
    /// معرف العقار
    /// PropertyId
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// معرف المالك
    /// OwnerId
    /// </summary>
    public Guid OwnerId { get; set; }
} 