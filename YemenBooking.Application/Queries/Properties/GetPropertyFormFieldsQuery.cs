namespace YemenBooking.Application.Queries.Properties;

using System;
using System.Collections.Generic;
using MediatR;
using YemenBooking.Application.DTOs;

/// <summary>
/// جلب حقول النموذج لنوع العقار
/// Get form fields grouped by groups for a property type
/// </summary>
public class GetPropertyFormFieldsQuery : IRequest<List<FieldGroupWithFieldsDto>>
{
    /// <summary>
    /// معرف نوع العقار
    /// PropertyTypeId
    /// </summary>
    public Guid PropertyTypeId { get; set; }

    /// <summary>
    /// عرض خاصة بالمالك (اختياري)
    /// OwnerView
    /// </summary>
    public bool OwnerView { get; set; } = true;
} 