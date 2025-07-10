namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان المراجعة
/// Review entity
/// </summary>
public class Review : BaseEntity
{
    /// <summary>
    /// معرف الحجز
    /// Booking identifier
    /// </summary>
    public Guid BookingId { get; set; }
    
    /// <summary>
    /// تقييم النظافة
    /// Cleanliness rating
    /// </summary>
    public int Cleanliness { get; set; }
    
    /// <summary>
    /// تقييم الخدمة
    /// Service rating
    /// </summary>
    public int Service { get; set; }
    
    /// <summary>
    /// تقييم الموقع
    /// Location rating
    /// </summary>
    public int Location { get; set; }
    
    /// <summary>
    /// تقييم القيمة
    /// Value rating
    /// </summary>
    public int Value { get; set; }
    
    /// <summary>
    /// تعليق المراجعة
    /// Review comment
    /// </summary>
    public string Comment { get; set; }
    
    /// <summary>
    /// تاريخ إنشاء المراجعة
    /// Review creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// نص رد المراجعة
    /// Review response text
    /// </summary>
    public string? ResponseText { get; set; }

    /// <summary>
    /// تاريخ رد المراجعة
    /// Review response date
    /// </summary>
    public DateTime? ResponseDate { get; set; }

    /// <summary>
    /// هل الرد في انتظار الموافقة
    /// Is pending approval for moderation
    /// </summary>
    public bool IsPendingApproval { get; set; } = true;
    
    /// <summary>
    /// الحجز المرتبط بالمراجعة
    /// Booking associated with the review
    /// </summary>
    public virtual Booking Booking { get; set; }
    
    /// <summary>
    /// العقار المرتبط بالمراجعة من خلال الحجز
    /// Property associated with the review through booking
    /// </summary>
    public virtual Property Property { get; set; }

    /// <summary>
    /// صور المراجعة المرتبطة
    /// Review images associated with the review
    /// </summary>
    public virtual ICollection<ReviewImage> Images { get; set; } = new List<ReviewImage>();
} 