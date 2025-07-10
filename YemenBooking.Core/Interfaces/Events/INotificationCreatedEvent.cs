using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء إشعار
/// Event for notification creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء إشعار جديد
/// This event is triggered when a new notification is created
/// </remarks>
public interface INotificationCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الإشعار الجديد
    /// ID of the new notification
    /// </summary>
    Guid NotificationId { get; }

    /// <summary>
    /// نوع الإشعار
    /// Notification type
    /// </summary>
    string Type { get; }

    /// <summary>
    /// عنوان الإشعار
    /// Notification title
    /// </summary>
    string Title { get; }

    /// <summary>
    /// محتوى الإشعار
    /// Notification message
    /// </summary>
    string Message { get; }

    /// <summary>
    /// معرف المستلم
    /// Recipient identifier
    /// </summary>
    Guid RecipientId { get; }

    /// <summary>
    /// معرف المرسل (إن وجد)
    /// Sender identifier (if any)
    /// </summary>
    Guid? SenderId { get; }

    /// <summary>
    /// تاريخ إنشاء الإشعار
    /// Notification creation date
    /// </summary>
    DateTime CreatedAt { get; }
} 