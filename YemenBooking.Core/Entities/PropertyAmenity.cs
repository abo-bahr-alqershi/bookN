namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.ValueObjects;

/// <summary>
/// كيان وسيلة العقار
/// Property Amenity entity
/// </summary>
public class PropertyAmenity : BaseEntity
{
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// معرف وسيلة نوع العقار
    /// Property type amenity identifier
    /// </summary>
    public Guid PtaId { get; set; }
    
    /// <summary>
    /// هل الوسيلة متاحة
    /// Is amenity available
    /// </summary>
    public bool IsAvailable { get; set; }
    
    /// <summary>
    /// التكلفة الإضافية للوسيلة
    /// Extra cost for the amenity
    /// </summary>
    public Money ExtraCost { get; set; }
    
    /// <summary>
    /// العقار المرتبط
    /// Property associated
    /// </summary>
    public virtual Property Property { get; set; }
    
    /// <summary>
    /// وسيلة نوع العقار المرتبطة
    /// Property type amenity associated
    /// </summary>
    public virtual PropertyTypeAmenity PropertyTypeAmenity { get; set; }
} 