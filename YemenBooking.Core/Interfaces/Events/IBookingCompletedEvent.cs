using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إكمال الحجز
/// Booking completed event
/// </summary>
public interface IBookingCompletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز المُكمل
    /// Completed booking identifier
    /// </summary>
    Guid CompletedBookingId { get; }

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
    /// تاريخ الإكمال الفعلي
    /// Actual completion date
    /// </summary>
    DateTime ActualCheckOutDate { get; }

    /// <summary>
    /// تاريخ الخروج المخطط
    /// Planned check-out date
    /// </summary>
    DateTime PlannedCheckOutDate { get; }

    /// <summary>
    /// تاريخ الدخول الفعلي
    /// Actual check-in date
    /// </summary>
    DateTime ActualCheckInDate { get; }

    /// <summary>
    /// المبلغ النهائي المدفوع
    /// Final paid amount
    /// </summary>
    decimal FinalAmount { get; }

    /// <summary>
    /// معرف من قام بالإكمال
    /// Who completed the booking
    /// </summary>
    Guid? CompletedBy { get; }

    /// <summary>
    /// تقييم العميل (إن وجد)
    /// Customer rating (if any)
    /// </summary>
    decimal? CustomerRating { get; }

    /// <summary>
    /// ملاحظات الإكمال
    /// Completion notes
    /// </summary>
    string? CompletionNotes { get; }

    /// <summary>
    /// إجمالي الليالي الفعلية
    /// Total actual nights
    /// </summary>
    int ActualNights { get; }
}
