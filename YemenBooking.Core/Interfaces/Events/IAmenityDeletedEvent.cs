using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف مرفق
/// Event for amenity deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف مرفق من النظام
/// This event is triggered when an amenity is deleted from the system
/// </remarks>
public interface IAmenityDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المرفق المحذوف
    /// Deleted amenity ID
    /// </summary>
    Guid AmenityId { get; }

    /// <summary>
    /// معرف المستخدم الذي حذف المرفق
    /// ID of the user who deleted the amenity
    /// </summary>
    new Guid UserId { get; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }
} 