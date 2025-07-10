namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;
using System.Collections.Generic;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان الحجز
/// Booking entity
/// </summary>
public class Booking : BaseEntity
{
    /// <summary>
    /// معرف المستخدم
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// معرف الوحدة
    /// Unit identifier
    /// </summary>
    public Guid UnitId { get; set; }
    
    /// <summary>
    /// تاريخ الوصول
    /// Check-in date
    /// </summary>
    public DateTime CheckIn { get; set; }
    
    /// <summary>
    /// تاريخ المغادرة
    /// Check-out date
    /// </summary>
    public DateTime CheckOut { get; set; }
    
    /// <summary>
    /// عدد الضيوف
    /// Number of guests
    /// </summary>
    public int GuestsCount { get; set; }
    
    /// <summary>
    /// السعر الإجمالي للحجز
    /// Total price of the booking
    /// </summary>
    public Money TotalPrice { get; set; }
    
    /// <summary>
    /// حالة الحجز (مؤكّد، معلّق، ملغى)
    /// Booking status (Confirmed, Pending, Cancelled)
    /// </summary>
    public BookingStatus Status { get; set; }
    
    /// <summary>
    /// تاريخ الحجز
    /// Booking date
    /// </summary>
    public DateTime BookedAt { get; set; }

    /// <summary>
    /// مصدر الحجز (ويب، موبايل، حجز مباشر)
    /// Booking source (WebApp, MobileApp, WalkIn)
    /// </summary>
    public string? BookingSource { get; set; }

    /// <summary>
    /// سبب الإلغاء
    /// Cancellation reason
    /// </summary>
    public string? CancellationReason { get; set; }

    /// <summary>
    /// هل الحجز مباشر (Walk-in)
    /// Is walk-in booking
    /// </summary>
    public bool IsWalkIn { get; set; } = false;
    
    /// <summary>
    /// المستخدم المرتبط بالحجز
    /// User associated with the booking
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// الوحدة المرتبطة بالحجز
    /// Unit associated with the booking
    /// </summary>
    public virtual Unit Unit { get; set; }
    
    /// <summary>
    /// المدفوعات المرتبطة بالحجز
    /// Payments associated with the booking
    /// </summary>
    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    
    /// <summary>
    /// الخدمات المرتبطة بالحجز
    /// Services associated with the booking
    /// </summary>
    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
    
    /// <summary>
    /// المراجعات المرتبطة بالحجز
    /// Reviews associated with the booking
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    /// <summary>
    /// مبلغ عمولة المنصة
    /// Platform commission amount
    /// </summary>
    public decimal PlatformCommissionAmount { get; set; }

    /// <summary>
    /// تاريخ تسجيل الوصول الفعلي
    /// Actual check-in date
    /// </summary>
    public DateTime? ActualCheckInDate { get; set; }

    /// <summary>
    /// تاريخ المغادرة الفعلي
    /// Actual check-out date
    /// </summary>
    public DateTime? ActualCheckOutDate { get; set; }

    /// <summary>
    /// المبلغ النهائي المدفوع
    /// Final amount
    /// </summary>
    public decimal FinalAmount { get; set; }

    /// <summary>
    /// تقييم العميل
    /// Customer rating
    /// </summary>
    public decimal? CustomerRating { get; set; }

    /// <summary>
    /// ملاحظات إكمال الحجز
    /// Completion notes
    /// </summary>
    public string? CompletionNotes { get; set; }
} 