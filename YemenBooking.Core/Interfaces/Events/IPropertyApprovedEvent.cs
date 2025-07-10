using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث الموافقة على العقار
/// Property approved event
/// </summary>
public interface IPropertyApprovedEvent : IDomainEvent
{
    /// <summary>
    /// معرف العقار المُوافق عليه
    /// Approved property identifier
    /// </summary>
    Guid ApprovedPropertyId { get; }

    /// <summary>
    /// معرف المسؤول الذي وافق على العقار
    /// Admin who approved the property
    /// </summary>
    Guid ApprovedBy { get; }

    /// <summary>
    /// تاريخ الموافقة
    /// Approval date
    /// </summary>
    DateTime ApprovedAt { get; }

    /// <summary>
    /// ملاحظات الموافقة
    /// Approval notes
    /// </summary>
    string? ApprovalNotes { get; }

    /// <summary>
    /// معرف المالك
    /// Owner identifier
    /// </summary>
    Guid OwnerId { get; }
}
