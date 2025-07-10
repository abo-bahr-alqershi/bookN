using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث فشل عملية دفع
/// Event for payment failure
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند فشل عملية دفع
/// This event is triggered when a payment operation fails
/// </remarks>
public interface IPaymentFailedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الدفع الفاشل
    /// ID of the failed payment
    /// </summary>
    Guid PaymentId { get; }
    
    /// <summary>
    /// معرف الحجز
    /// Booking ID
    /// </summary>
    Guid BookingId { get; }
        
    /// <summary>
    /// المبلغ المحاول دفعه
    /// Amount attempted to pay
    /// </summary>
    Money AttemptedAmount { get; }
    
    /// <summary>
    /// طريقة الدفع المستخدمة
    /// Payment method used
    /// </summary>
    string Method { get; }
    
    /// <summary>
    /// سبب فشل الدفع
    /// Payment failure reason
    /// </summary>
    string FailureReason { get; }
    
    /// <summary>
    /// رمز الخطأ من بوابة الدفع
    /// Error code from payment gateway
    /// </summary>
    string? ErrorCode { get; }
    
    /// <summary>
    /// تاريخ محاولة الدفع
    /// Payment attempt date
    /// </summary>
    DateTime AttemptedAt { get; }
}
