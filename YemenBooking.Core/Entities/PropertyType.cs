namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان نوع العقار
/// Property Type entity
/// </summary>
public class PropertyType : BaseEntity
{
    /// <summary>
    /// اسم نوع العقار (فندق، شاليه، استراحة، فيلا، شقة)
    /// Property type name (Hotel, Chalet, Rest House, Villa, Apartment)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// وصف نوع العقار
    /// Property type description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// المرافق الافتراضية لنوع العقار (JSON)
    /// Default amenities for the property type (JSON)
    /// </summary>
    public string DefaultAmenities { get; set; }
    
    /// <summary>
    /// العقارات المرتبطة بهذا النوع
    /// Properties associated with this type
    /// </summary>
    public virtual ICollection<Property> Properties { get; set; } = new List<Property>();
    
    /// <summary>
    /// أنواع الوحدات المرتبطة بهذا النوع من العقارات
    /// Unit types associated with this property type
    /// </summary>
    public virtual ICollection<UnitType> UnitTypes { get; set; } = new List<UnitType>();
    
    /// <summary>
    /// الوسائل المرتبطة بنوع العقار
    /// Amenities associated with this property type
    /// </summary>
    public virtual ICollection<PropertyTypeAmenity> PropertyTypeAmenities { get; set; } = new List<PropertyTypeAmenity>();

} 