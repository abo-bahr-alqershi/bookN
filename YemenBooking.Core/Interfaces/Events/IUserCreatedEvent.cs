using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء مستخدم جديد
/// User created event
/// </summary>
public interface IUserCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المستخدم الجديد
    /// New user identifier
    /// </summary>
    Guid NewUserId { get; }

    /// <summary>
    /// البريد الإلكتروني للمستخدم
    /// User email
    /// </summary>
    string Email { get; }

    /// <summary>
    /// اسم المستخدم
    /// User name
    /// </summary>
    string FullName { get; }

    /// <summary>
    /// رقم الهاتف
    /// Phone number
    /// </summary>
    string? PhoneNumber { get; }

    /// <summary>
    /// هل تم تفعيل الحساب
    /// Is account activated
    /// </summary>
    bool IsActive { get; }
}
