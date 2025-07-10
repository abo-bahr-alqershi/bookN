using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إلغاء تفعيل المستخدم
/// User deactivated event
/// </summary>
public interface IUserDeactivatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المستخدم المُلغى تفعيله
    /// Deactivated user identifier
    /// </summary>
    Guid DeactivatedUserId { get; }

    /// <summary>
    /// سبب إلغاء التفعيل
    /// Deactivation reason
    /// </summary>
    string? Reason { get; }

    /// <summary>
    /// معرف المسؤول الذي قام بإلغاء التفعيل
    /// Admin who performed the deactivation
    /// </summary>
    Guid? DeactivatedBy { get; }

    /// <summary>
    /// تاريخ إلغاء التفعيل
    /// Deactivation date
    /// </summary>
    DateTime DeactivatedAt { get; }
}
