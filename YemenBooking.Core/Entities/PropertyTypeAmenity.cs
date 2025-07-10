namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان وسيلة نوع العقار
/// Property Type Amenity entity
/// </summary>
public class PropertyTypeAmenity : BaseEntity
{
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid PropertyTypeId { get; set; }
    
    /// <summary>
    /// معرف الوسيلة
    /// Amenity identifier
    /// </summary>
    public Guid AmenityId { get; set; }
    
    /// <summary>
    /// هل هي وسيلة افتراضية
    /// Is default amenity
    /// </summary>
    public bool IsDefault { get; set; }
    
    /// <summary>
    /// نوع العقار المرتبط
    /// Property type associated
    /// </summary>
    public virtual PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// الوسيلة المرتبطة
    /// Amenity associated
    /// </summary>
    public virtual Amenity Amenity { get; set; }
    
    /// <summary>
    /// وسائل العقار المرتبطة
    /// Property amenities associated
    /// </summary>
    public virtual ICollection<PropertyAmenity> PropertyAmenities { get; set; } = new List<PropertyAmenity>();
} 