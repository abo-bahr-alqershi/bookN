namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان الدفع
/// Payment entity
/// </summary>
public class Payment : BaseEntity
{
    /// <summary>
    /// معرف الحجز
    /// Booking identifier
    /// </summary>
    public Guid BookingId { get; set; }
    
    /// <summary>
    /// المبلغ المدفوع
    /// Paid amount
    /// </summary>
    public Money Amount { get; set; }
    
    /// <summary>
    /// طريقة الدفع (بطاقة، نقدي، محفظة)
    /// Payment method (Card, Cash, Wallet)
    /// </summary>
    public PaymentMethod Method { get; set; }
    
    /// <summary>
    /// معرف المعاملة
    /// Transaction identifier
    /// </summary>
    public string TransactionId { get; set; }
    
    /// <summary>
    /// حالة الدفع (ناجح، فاشل، معلّق)
    /// Payment status (Successful, Failed, Pending)
    /// </summary>
    public PaymentStatus Status { get; set; }
    
    /// <summary>
    /// تاريخ الدفع
    /// Payment date
    /// </summary>
    public DateTime PaymentDate { get; set; }
    
    /// <summary>
    /// معرف المعاملة في بوابة الدفع
    /// Gateway transaction identifier
    /// </summary>
    public string GatewayTransactionId { get; set; } = string.Empty;

    /// <summary>
    /// معرف المستخدم الذي قام بمعالجة الدفع
    /// User ID who processed the payment
    /// </summary>
    public Guid ProcessedBy { get; set; }
    
    /// <summary>
    /// الحجز المرتبط بالدفع
    /// Booking associated with the payment
    /// </summary>
    public virtual Booking Booking { get; set; }
} 