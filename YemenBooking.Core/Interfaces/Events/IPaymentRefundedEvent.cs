using YemenBooking.Core.Enums;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// واجهة حدث استرداد الدفع
/// Interface for payment refunded event
/// </summary>
/// <remarks>
/// يتم تنفيذ هذا الحدث عند استرداد مبلغ من دفعة
/// This event is triggered when a payment amount is refunded
/// </remarks>
public interface IPaymentRefundedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الدفع الأصلي
    /// Original payment identifier
    /// </summary>
    Guid PaymentId { get; }

    /// <summary>
    /// معرف الحجز المرتبط بالدفع
    /// Booking ID associated with the payment
    /// </summary>
    Guid BookingId { get; }

    /// <summary>
    /// المبلغ المسترد
    /// Refunded amount
    /// </summary>
    decimal RefundAmount { get; }

    /// <summary>
    /// سبب الاسترداد
    /// Refund reason
    /// </summary>
    string RefundReason { get; }

    /// <summary>
    /// معرف معاملة الاسترداد
    /// Refund transaction ID
    /// </summary>
    string RefundTransactionId { get; }

    /// <summary>
    /// تاريخ ووقت الاسترداد
    /// Refund date and time
    /// </summary>
    DateTime RefundedAt { get; }

    /// <summary>
    /// طريقة الاسترداد
    /// Refund method
    /// </summary>
    PaymentMethod RefundMethod { get; }

    /// <summary>
    /// حالة الاسترداد
    /// Refund status
    /// </summary>
    PaymentStatus RefundStatus { get; }

    /// <summary>
    /// المبلغ الأصلي للدفع
    /// Original payment amount
    /// </summary>
    decimal OriginalAmount { get; }

    /// <summary>
    /// العملة المستخدمة
    /// Currency used
    /// </summary>
    string Currency { get; }

    /// <summary>
    /// ملاحظات إضافية
    /// Additional notes
    /// </summary>
    string? Notes { get; }
}
