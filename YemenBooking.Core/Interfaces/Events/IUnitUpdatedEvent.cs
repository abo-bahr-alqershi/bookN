using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث بيانات الوحدة
/// Unit updated event
/// </summary>
public interface IUnitUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الوحدة المحدثة
    /// Updated unit identifier
    /// </summary>
    Guid UpdatedUnitId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

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

    /// <summary>
    /// معرف من قام بالتحديث
    /// Who performed the update
    /// </summary>
    Guid? UpdatedBy { get; }
}
