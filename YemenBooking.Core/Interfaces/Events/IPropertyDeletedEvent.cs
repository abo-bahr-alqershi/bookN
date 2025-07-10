using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف العقار
/// Property deleted event
/// </summary>
public interface IPropertyDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف العقار المحذوف
    /// Deleted property identifier
    /// </summary>
    Guid DeletedPropertyId { get; }

    /// <summary>
    /// اسم العقار المحذوف
    /// Deleted property name
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// معرف المالك
    /// Owner identifier
    /// </summary>
    Guid OwnerId { get; }

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
    /// عدد الوحدات المحذوفة مع العقار
    /// Number of units deleted with property
    /// </summary>
    int DeletedUnitsCount { get; }
}
