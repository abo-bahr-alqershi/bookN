namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;
using System.Collections.Generic;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان الوحدة
/// Unit entity
/// </summary>
public class Unit : BaseEntity
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// معرف نوع الوحدة
    /// Unit type identifier
    /// </summary>
    public Guid UnitTypeId { get; set; }
    
    /// <summary>
    /// اسم الوحدة (الوحدة A، الجناح الملكي)
    /// Unit name (Unit A, Royal Suite)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// السعر الأساسي للوحدة
    /// Base price of the unit
    /// </summary>
    public Money BasePrice { get; set; }
    
    /// <summary>
    /// السعة القصوى للوحدة (عدد الضيوف الأقصى)
    /// Maximum capacity of the unit (max number of guests)
    /// </summary>
    public int MaxCapacity { get; set; }
    
    /// <summary>
    /// الميزات المخصصة للوحدة (JSON)
    /// Custom features of the unit (JSON)
    /// </summary>
    public string CustomFeatures { get; set; }
    
    /// <summary>
    /// حالة توفر الوحدة
    /// Unit availability status
    /// </summary>
    public bool IsAvailable { get; set; }

    /// <summary>
    /// عدد مرات المشاهدة
    /// View count
    /// </summary>
    public int ViewCount { get; set; } = 0;

    /// <summary>
    /// عدد الحجوزات
    /// Booking count
    /// </summary>
    public int BookingCount { get; set; } = 0;
    
    /// <summary>
    /// العقار المرتبط بالوحدة
    /// Property associated with the unit
    /// </summary>
    public virtual Property Property { get; set; }
    
    /// <summary>
    /// نوع الوحدة المرتبط
    /// Unit type associated
    /// </summary>
    public virtual UnitType UnitType { get; set; }
    
    /// <summary>
    /// الحجوزات المرتبطة بالوحدة
    /// Bookings associated with the unit
    /// </summary>
    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    
    /// <summary>
    /// الصور المرتبطة بالوحدة
    /// Images associated with the unit
    /// </summary>
    public virtual ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();

    /// <summary>
    /// قيم الحقول الخاصة بالوحدة
    /// Field values associated with this unit
    /// </summary>
    public virtual ICollection<UnitFieldValue> FieldValues { get; set; } = new List<UnitFieldValue>();

    /// <summary>
    /// طريقة حساب السعر (بالساعة، اليوم، الأسبوع، الشهر)
    /// Pricing calculation method (Hourly, Daily, Weekly, Monthly)
    /// </summary>
    public PricingMethod PricingMethod { get; set; }
} 