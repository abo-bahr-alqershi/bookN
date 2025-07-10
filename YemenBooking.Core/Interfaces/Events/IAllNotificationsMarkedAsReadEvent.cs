using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث وضع علامة قراءة على جميع الإشعارات
/// Event for marking all notifications as read
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند وضع علامة قراءة على جميع إشعارات المستخدم
/// This event is triggered when all notifications for a user are marked as read
/// </remarks>
public interface IAllNotificationsMarkedAsReadEvent : IDomainEvent
{
    /// <summary>
    /// معرف المستخدم المستلم
    /// Recipient identifier
    /// </summary>
    Guid RecipientId { get; }

    /// <summary>
    /// معرف المستخدم الذي قام بالعملية
    /// ID of the user who performed the action
    /// </summary>
    new Guid UserId { get; }

    /// <summary>
    /// تاريخ تنفيذ العملية
    /// Action execution date
    /// </summary>
    DateTime ReadAt { get; }
} 