using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء عقار جديد
/// Property created event
/// </summary>
public interface IPropertyCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف العقار الجديد
    /// New property identifier
    /// </summary>
    Guid NewPropertyId { get; }

    /// <summary>
    /// اسم العقار
    /// Property name
    /// </summary>
    string PropertyName { get; }

    /// <summary>
    /// معرف المالك
    /// Owner identifier
    /// </summary>
    Guid OwnerId { get; }

    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    Guid PropertyTypeId { get; }

    /// <summary>
    /// العنوان
    /// Address
    /// </summary>
    string Address { get; }

    /// <summary>
    /// المدينة
    /// City
    /// </summary>
    string City { get; }

    /// <summary>
    /// الدولة
    /// Country
    /// </summary>
    string Country { get; }

    /// <summary>
    /// حالة العقار
    /// Property status
    /// </summary>
    string Status { get; }
}
