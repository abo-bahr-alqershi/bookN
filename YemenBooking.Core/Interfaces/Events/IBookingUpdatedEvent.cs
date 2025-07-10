using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث حجز
/// Event for booking update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث تفاصيل حجز موجود
/// This event is triggered when an existing booking's details are updated
/// </remarks>
public interface IBookingUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز المحدث
    /// ID of the updated booking
    /// </summary>
    Guid BookingId { get; }
        
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// تاريخ الوصول الجديد (إن تم تحديثه)
    /// New check-in date (if updated)
    /// </summary>
    DateTime? NewCheckIn { get; }
    
    /// <summary>
    /// تاريخ المغادرة الجديد (إن تم تحديثه)
    /// New check-out date (if updated)
    /// </summary>
    DateTime? NewCheckOut { get; }
    
    /// <summary>
    /// عدد الضيوف الجديد (إن تم تحديثه)
    /// New guests count (if updated)
    /// </summary>
    int? NewGuestsCount { get; }
    
    /// <summary>
    /// السعر الإجمالي الجديد (إن تم تحديثه)
    /// New total price (if updated)
    /// </summary>
    Money? NewTotalPrice { get; }
    
    /// <summary>
    /// الحالة الجديدة (إن تم تحديثها)
    /// New status (if updated)
    /// </summary>
    string? NewStatus { get; }
}
