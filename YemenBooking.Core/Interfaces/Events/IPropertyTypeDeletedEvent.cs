using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف نوع العقار
/// Event for property type deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف نوع عقار من النظام
/// This event is triggered when a property type is deleted from the system
/// </remarks>
public interface IPropertyTypeDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع العقار المحذوف
    /// Deleted property type ID
    /// </summary>
    Guid TypeId { get; }

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