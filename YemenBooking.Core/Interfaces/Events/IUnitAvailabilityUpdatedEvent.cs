using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث توفر الوحدة
/// Unit availability updated event
/// </summary>
public interface IUnitAvailabilityUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الوحدة
    /// Unit identifier
    /// </summary>
    Guid UnitId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

    /// <summary>
    /// تاريخ البداية للفترة المحدثة
    /// Start date of updated period
    /// </summary>
    DateTime FromDate { get; }

    /// <summary>
    /// تاريخ النهاية للفترة المحدثة
    /// End date of updated period
    /// </summary>
    DateTime ToDate { get; }

    /// <summary>
    /// الحالة الجديدة للتوفر
    /// New availability status
    /// </summary>
    bool IsAvailable { get; }

    /// <summary>
    /// الحالة السابقة للتوفر
    /// Previous availability status
    /// </summary>
    bool? PreviousAvailability { get; }

    /// <summary>
    /// معرف من قام بالتحديث
    /// Who performed the update
    /// </summary>
    Guid? UpdatedBy { get; }

    /// <summary>
    /// سبب التحديث
    /// Update reason
    /// </summary>
    string? UpdateReason { get; }
}
