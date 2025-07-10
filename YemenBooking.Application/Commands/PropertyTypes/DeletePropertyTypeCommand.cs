using System;
using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Commands.PropertyTypes;

/// <summary>
/// أمر لحذف نوع العقار
/// Command to delete a property type
/// </summary>
public class DeletePropertyTypeCommand : IRequest<ResultDto<bool>>
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid PropertyTypeId { get; set; }
} 