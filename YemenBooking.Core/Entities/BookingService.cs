namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;

/// <summary>
/// كيان خدمة الحجز
/// Booking Service entity
/// </summary>
public class BookingService : BaseEntity
{
    /// <summary>
    /// معرف الحجز
    /// Booking identifier
    /// </summary>
    public Guid BookingId { get; set; }
    
    /// <summary>
    /// معرف الخدمة
    /// Service identifier
    /// </summary>
    public Guid ServiceId { get; set; }
    
    /// <summary>
    /// الكمية
    /// Quantity
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// السعر الإجمالي للخدمة
    /// Total price of the service
    /// </summary>
    public Money TotalPrice { get; set; }
    
    /// <summary>
    /// الحجز المرتبط بخدمة الحجز
    /// Booking associated with the booking service
    /// </summary>
    public virtual Booking Booking { get; set; }
    
    /// <summary>
    /// الخدمة المرتبطة بخدمة الحجز
    /// Service associated with the booking service
    /// </summary>
    public virtual PropertyService Service { get; set; }
} 