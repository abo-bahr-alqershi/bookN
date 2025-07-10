using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تسجيل الوصول للحجز
/// Event for booking check-in
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تسجيل وصول الضيف للحجز
/// This event is triggered when a guest checks in for a booking
/// </remarks>
public interface IBookingCheckedInEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز
    /// Booking identifier
    /// </summary>
    Guid BookingId { get; }

    /// <summary>
    /// تاريخ ووقت تسجيل الوصول
    /// Check-in date and time
    /// </summary>
    DateTime CheckedInAt { get; }
} 