using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تخصيص دور للمستخدم
/// User role assigned event
/// </summary>
public interface IUserRoleAssignedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المستخدم
    /// User identifier
    /// </summary>
    Guid AssignedUserId { get; }

    /// <summary>
    /// معرف الدور المخصص
    /// Assigned role identifier
    /// </summary>
    Guid RoleId { get; }

    /// <summary>
    /// اسم الدور
    /// Role name
    /// </summary>
    string RoleName { get; }

    /// <summary>
    /// معرف المسؤول الذي قام بالتخصيص
    /// Admin who performed the assignment
    /// </summary>
    Guid? AssignedBy { get; }

    /// <summary>
    /// تاريخ التخصيص
    /// Assignment date
    /// </summary>
    DateTime AssignedAt { get; }
}
