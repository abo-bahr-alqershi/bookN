using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء وحدة جديدة
/// Unit created event
/// </summary>
public interface IUnitCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الوحدة الجديدة
    /// New unit identifier
    /// </summary>
    Guid NewUnitId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

    /// <summary>
    /// معرف نوع الوحدة
    /// Unit type identifier
    /// </summary>
    Guid UnitTypeId { get; }

    /// <summary>
    /// اسم الوحدة
    /// Unit name
    /// </summary>
    string UnitName { get; }

    /// <summary>
    /// رقم الوحدة
    /// Unit number
    /// </summary>
    string? UnitNumber { get; }

    /// <summary>
    /// السعر الأساسي
    /// Base price
    /// </summary>
    decimal BasePrice { get; }

    /// <summary>
    /// الحد الأقصى للضيوف
    /// Maximum guests
    /// </summary>
    int MaxGuests { get; }

    /// <summary>
    /// معرف من أنشأ الوحدة
    /// Who created the unit
    /// </summary>
    Guid? CreatedBy { get; }
}
