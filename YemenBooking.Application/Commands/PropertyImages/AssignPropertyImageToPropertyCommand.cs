using System;
using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Commands.PropertyImages;

/// <summary>
/// أمر لتعيين صورة إلى عقار
/// Command to assign an image to a property
/// </summary>
public class AssignPropertyImageToPropertyCommand : IRequest<ResultDto<bool>>
{
    /// <summary>
    /// معرف الصورة
    /// Image identifier
    /// </summary>
    public Guid ImageId { get; set; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }

    /// <summary>
    /// تعيين كصورة رئيسية
    /// Property identifier
    /// </summary>
    public bool SetAsMain { get; set; } = false;

} 