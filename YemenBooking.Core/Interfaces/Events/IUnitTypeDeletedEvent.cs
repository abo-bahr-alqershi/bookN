using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف نوع الوحدة
/// Event for unit type deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف نوع وحدة من النظام
/// This event is triggered when a unit type is deleted from the system
/// </remarks>
public interface IUnitTypeDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع الوحدة المحذوف
    /// Deleted unit type ID
    /// </summary>
    Guid UnitTypeId { get; }

    /// <summary>
    /// معرف المستخدم الذي حذف النوع
    /// ID of the user who deleted the type
    /// </summary>
    new Guid UserId { get; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }
} 