using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث بيانات العقار
/// Property updated event
/// </summary>
public interface IPropertyUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف العقار المحدث
    /// Updated property identifier
    /// </summary>
    Guid UpdatedPropertyId { get; }

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
