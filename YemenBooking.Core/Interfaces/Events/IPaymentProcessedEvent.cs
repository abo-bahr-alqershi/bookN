using YemenBooking.Core.Interfaces;
using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث معالجة الدفع
/// Payment processed event
/// </summary>
public interface IPaymentProcessedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الدفع الفريد
    /// Unique payment identifier
    /// </summary>
    Guid PaymentId { get; }

    /// <summary>
    /// معرف الحجز المرتبط بالدفع
    /// Booking ID associated with the payment
    /// </summary>
    Guid BookingId { get; }

    /// <summary>
    /// المبلغ المدفوع
    /// Payment amount
    /// </summary>
    Money Amount { get; }

    /// <summary>
    /// طريقة الدفع المستخدمة
    /// Payment method used
    /// </summary>
    string Method { get; }

    /// <summary>
    /// معرف المعاملة
    /// Transaction identifier
    /// </summary>
    string TransactionId { get; }

    /// <summary>
    /// حالة الدفع
    /// Payment status
    /// </summary>
    string Status { get; }

    /// <summary>
    /// تاريخ ووقت معالجة الدفع
    /// Payment processing date and time
    /// </summary>
    DateTime ProcessedAt { get; }

    /// <summary>
    /// العملة المستخدمة
    /// Currency used
    /// </summary>
    string Currency { get; }

    /// <summary>
    /// معلومات إضافية عن المعاملة
    /// Additional transaction information
    /// </summary>
    string? Notes { get; }
}
