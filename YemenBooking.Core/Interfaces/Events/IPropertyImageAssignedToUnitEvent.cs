using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تعيين صورة إلى وحدة
/// Event for assigning image to unit
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تعيين صورة معينة إلى وحدة
/// This event is triggered when an image is assigned to a unit
/// </remarks>
public interface IPropertyImageAssignedToUnitEvent : IDomainEvent
{
    /// <summary>
    /// معرف الصورة
    /// Image identifier
    /// </summary>
    Guid ImageId { get; }

    /// <summary>
    /// معرف الوحدة
    /// Unit identifier
    /// </summary>
    Guid UnitId { get; }

    /// <summary>
    /// تاريخ التعيين
    /// Assignment date
    /// </summary>
    DateTime AssignedAt { get; }
} 