using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء حجز جديد
/// Event for booking creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء حجز جديد بنجاح
/// This event is triggered when a new booking is successfully created
/// </remarks>
public interface IBookingCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز الذي تم إنشاؤه
    /// ID of the created booking
    /// </summary>
    Guid BookingId { get; }
        
    /// <summary>
    /// معرف الوحدة المحجوزة
    /// ID of the booked unit
    /// </summary>
    Guid UnitId { get; }
    
    /// <summary>
    /// تاريخ الوصول
    /// Check-in date
    /// </summary>
    DateTime CheckIn { get; }
    
    /// <summary>
    /// تاريخ المغادرة
    /// Check-out date
    /// </summary>
    DateTime CheckOut { get; }
    
    /// <summary>
    /// عدد الضيوف
    /// Number of guests
    /// </summary>
    int GuestsCount { get; }
    
    /// <summary>
    /// السعر الإجمالي للحجز
    /// Total price of the booking
    /// </summary>
    Money TotalPrice { get; }
    
    /// <summary>
    /// حالة الحجز
    /// Booking status
    /// </summary>
    string Status { get; }
    
    /// <summary>
    /// تاريخ الحجز
    /// Booking date
    /// </summary>
    DateTime BookedAt { get; }
}
