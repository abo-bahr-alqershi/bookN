using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إلغاء حجز
/// Event for booking cancellation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إلغاء حجز موجود
/// This event is triggered when an existing booking is cancelled
/// </remarks>
public interface IBookingCancelledEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز الملغى
    /// ID of the cancelled booking
    /// </summary>
    Guid BookingId { get; }
        
    /// <summary>
    /// سبب الإلغاء
    /// Cancellation reason
    /// </summary>
    string CancellationReason { get; }
    
    /// <summary>
    /// نوع الإلغاء (من قبل المستخدم، من قبل النظام، من قبل المالك)
    /// Cancellation type (by user, by system, by owner)
    /// </summary>
    string CancellationType { get; }
    
    /// <summary>
    /// هل تم استرداد المبلغ
    /// Whether refund was processed
    /// </summary>
    bool RefundProcessed { get; }
    
    /// <summary>
    /// مبلغ الاسترداد (إن وجد)
    /// Refund amount (if any)
    /// </summary>
    Money? RefundAmount { get; }
    
    /// <summary>
    /// تاريخ الإلغاء
    /// Cancellation date
    /// </summary>
    DateTime CancelledAt { get; }
}
