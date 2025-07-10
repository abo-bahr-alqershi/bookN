using YemenBooking.Core.Enums;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// واجهة حدث تحديث حالة الدفع
/// Interface for payment status updated event
/// </summary>
/// <remarks>
/// يتم تنفيذ هذا الحدث عند تحديث حالة الدفع
/// This event is triggered when payment status is updated
/// </remarks>
public interface IPaymentStatusUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الدفع
    /// Payment identifier
    /// </summary>
    Guid PaymentId { get; }

    /// <summary>
    /// معرف الحجز المرتبط بالدفع
    /// Booking ID associated with the payment
    /// </summary>
    Guid BookingId { get; }

    /// <summary>
    /// الحالة السابقة للدفع
    /// Previous payment status
    /// </summary>
    PaymentStatus PreviousStatus { get; }

    /// <summary>
    /// الحالة الجديدة للدفع
    /// New payment status
    /// </summary>
    PaymentStatus NewStatus { get; }

    /// <summary>
    /// تاريخ ووقت تحديث الحالة
    /// Status update date and time
    /// </summary>
    DateTime UpdatedAt { get; }

    /// <summary>
    /// سبب تحديث الحالة
    /// Status update reason
    /// </summary>
    string? UpdateReason { get; }

    /// <summary>
    /// معرف المستخدم الذي قام بالتحديث
    /// User ID who performed the update
    /// </summary>
    Guid? UpdatedByUserId { get; }

    /// <summary>
    /// المبلغ المرتبط بالدفع
    /// Amount associated with payment
    /// </summary>
    decimal Amount { get; }

    /// <summary>
    /// معرف المعاملة
    /// Transaction ID
    /// </summary>
    string TransactionId { get; }

    /// <summary>
    /// ملاحظات إضافية
    /// Additional notes
    /// </summary>
    string? Notes { get; }
}
