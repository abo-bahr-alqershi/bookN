namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;
using System.Collections.Generic;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان خدمة العقار
/// Property Service entity
/// </summary>
public class PropertyService : BaseEntity
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// اسم الخدمة (نقل من المطار، سبا، غسيل ملابس)
    /// Service name (Airport Transfer, Spa, Laundry)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// سعر الخدمة
    /// Service price
    /// </summary>
    public Money Price { get; set; }
    
    /// <summary>
    /// نموذج التسعير (ثابت، للشخص، للليلة)
    /// Pricing model (Fixed, Per Person, Per Night)
    /// </summary>
    public PricingModel PricingModel { get; set; }
    
    /// <summary>
    /// العقار المرتبط بالخدمة
    /// Property associated with the service
    /// </summary>
    public virtual Property Property { get; set; }
    
    /// <summary>
    /// الحجوزات المرتبطة بهذه الخدمة
    /// Bookings associated with this service
    /// </summary>
    public virtual ICollection<BookingService> BookingServices { get; set; } = new List<BookingService>();
} 