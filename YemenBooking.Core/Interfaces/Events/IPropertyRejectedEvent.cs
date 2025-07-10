using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث رفض العقار
/// Property rejected event
/// </summary>
public interface IPropertyRejectedEvent : IDomainEvent
{
    /// <summary>
    /// معرف العقار المرفوض
    /// Rejected property identifier
    /// </summary>
    Guid RejectedPropertyId { get; }

    /// <summary>
    /// معرف المسؤول الذي رفض العقار
    /// Admin who rejected the property
    /// </summary>
    Guid RejectedBy { get; }

    /// <summary>
    /// سبب الرفض
    /// Rejection reason
    /// </summary>
    string RejectionReason { get; }

    /// <summary>
    /// تاريخ الرفض
    /// Rejection date
    /// </summary>
    DateTime RejectedAt { get; }

    /// <summary>
    /// معرف المالك
    /// Owner identifier
    /// </summary>
    Guid OwnerId { get; }

    /// <summary>
    /// ملاحظات إضافية
    /// Additional notes
    /// </summary>
    string? AdditionalNotes { get; }
}
