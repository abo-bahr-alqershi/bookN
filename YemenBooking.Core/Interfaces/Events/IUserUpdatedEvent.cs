using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث بيانات المستخدم
/// User updated event
/// </summary>
public interface IUserUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المستخدم المحدث
    /// Updated user identifier
    /// </summary>
    Guid UpdatedUserId { get; }

    /// <summary>
    /// البيانات القديمة (JSON)
    /// Old data (JSON)
    /// </summary>
    string? OldData { get; }

    /// <summary>
    /// البيانات الجديدة (JSON)
    /// New data (JSON)
    /// </summary>
    string? NewData { get; }

    /// <summary>
    /// الحقول التي تم تحديثها
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
}
