using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تعيين صورة إلى عقار
/// Event for assigning image to property
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تعيين صورة معينة إلى عقار
/// This event is triggered when an image is assigned to a property
/// </remarks>
public interface IPropertyImageAssignedToPropertyEvent : IDomainEvent
{
    /// <summary>
    /// معرف الصورة
    /// Image identifier
    /// </summary>
    Guid ImageId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

    /// <summary>
    /// تاريخ التعيين
    /// Assignment date
    /// </summary>
    DateTime AssignedAt { get; }
} 