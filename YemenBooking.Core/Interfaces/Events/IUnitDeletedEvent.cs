using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف الوحدة
/// Unit deleted event
/// </summary>
public interface IUnitDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الوحدة المحذوفة
    /// Deleted unit identifier
    /// </summary>
    Guid DeletedUnitId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

    /// <summary>
    /// اسم الوحدة المحذوفة
    /// Deleted unit name
    /// </summary>
    string UnitName { get; }

    /// <summary>
    /// رقم الوحدة المحذوفة
    /// Deleted unit number
    /// </summary>
    string? UnitNumber { get; }

    /// <summary>
    /// معرف من قام بالحذف
    /// Who performed the deletion
    /// </summary>
    Guid? DeletedBy { get; }

    /// <summary>
    /// سبب الحذف
    /// Deletion reason
    /// </summary>
    string? DeletionReason { get; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }

    /// <summary>
    /// هل كانت هناك حجوزات نشطة
    /// Were there active bookings
    /// </summary>
    bool HadActiveBookings { get; }
}
