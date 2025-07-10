using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إضافة خدمة إلى الحجز
/// Event for adding service to booking
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إضافة خدمة جديدة إلى حجز موجود
/// This event is triggered when a new service is added to an existing booking
/// </remarks>
public interface IServiceAddedToBookingEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز
    /// Booking ID
    /// </summary>
    Guid BookingId { get; }
    
    /// <summary>
    /// معرف الخدمة المضافة
    /// ID of the added service
    /// </summary>
    Guid ServiceId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أضاف الخدمة
    /// ID of the user who added the service
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الكمية المطلوبة من الخدمة
    /// Requested quantity of the service
    /// </summary>
    int Quantity { get; }
    
    /// <summary>
    /// السعر الإجمالي للخدمة
    /// Total price of the service
    /// </summary>
    Money TotalPrice { get; }
    
    /// <summary>
    /// تاريخ إضافة الخدمة
    /// Date when service was added
    /// </summary>
    DateTime AddedAt { get; }
}

/// <summary>
/// حدث إزالة خدمة من الحجز
/// Event for removing service from booking
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إزالة خدمة من حجز موجود
/// This event is triggered when a service is removed from an existing booking
/// </remarks>
public interface IServiceRemovedFromBookingEvent : IDomainEvent
{
    /// <summary>
    /// معرف الحجز
    /// Booking ID
    /// </summary>
    Guid BookingId { get; }
    
    /// <summary>
    /// معرف الخدمة المحذوفة
    /// ID of the removed service
    /// </summary>
    Guid ServiceId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أزال الخدمة
    /// ID of the user who removed the service
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الكمية التي تم إزالتها
    /// Quantity that was removed
    /// </summary>
    int RemovedQuantity { get; }
    
    /// <summary>
    /// المبلغ المرد
    /// Refunded amount
    /// </summary>
    Money RefundedAmount { get; }
    
    /// <summary>
    /// سبب الإزالة
    /// Removal reason
    /// </summary>
    string RemovalReason { get; }
    
    /// <summary>
    /// تاريخ إزالة الخدمة
    /// Date when service was removed
    /// </summary>
    DateTime RemovedAt { get; }
}
