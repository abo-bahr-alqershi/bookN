using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث وضع علامة قراءة على إشعار
/// Event for marking notification as read
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند وضع علامة قراءة على إشعار
/// This event is triggered when a notification is marked as read
/// </remarks>
public interface INotificationMarkedAsReadEvent : IDomainEvent
{
    /// <summary>
    /// معرف الإشعار
    /// Notification identifier
    /// </summary>
    Guid NotificationId { get; }

    /// <summary>
    /// معرف المستخدم الذي قرأ الإشعار
    /// ID of the user who read the notification
    /// </summary>
    new Guid UserId { get; }

    /// <summary>
    /// تاريخ القراءة
    /// Read date
    /// </summary>
    DateTime ReadAt { get; }
} 