using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تأكيد الحجز
/// Booking confirmed event
/// </summary>
public interface IBookingConfirmedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز المؤكد
    /// Confirmed booking identifier
    /// </summary>
    Guid ConfirmedBookingId { get; }

    /// <summary>
    /// معرف الوحدة
    /// Unit identifier
    /// </summary>
    Guid UnitId { get; }

    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    Guid PropertyId { get; }

    /// <summary>
    /// معرف من قام بالتأكيد
    /// Who confirmed the booking
    /// </summary>
    Guid? ConfirmedBy { get; }

    /// <summary>
    /// تاريخ التأكيد
    /// Confirmation date
    /// </summary>
    DateTime ConfirmedAt { get; }

    /// <summary>
    /// المبلغ المؤكد
    /// Confirmed amount
    /// </summary>
    decimal ConfirmedAmount { get; }

    /// <summary>
    /// تاريخ الدخول
    /// Check-in date
    /// </summary>
    DateTime CheckInDate { get; }

    /// <summary>
    /// تاريخ الخروج
    /// Check-out date
    /// </summary>
    DateTime CheckOutDate { get; }

    /// <summary>
    /// ملاحظات التأكيد
    /// Confirmation notes
    /// </summary>
    string? ConfirmationNotes { get; }
}
