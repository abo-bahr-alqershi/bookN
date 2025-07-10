namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان المرفق
/// Amenity entity
/// </summary>
public class Amenity : BaseEntity
{
    /// <summary>
    /// اسم المرفق (واي فاي، مسبح، جيم، إفطار)
    /// Amenity name (Wi-Fi, Pool, Gym, Breakfast)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// وصف المرفق
    /// Amenity description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// أنواع العقارات المرتبطة بهذا المرفق
    /// Property types associated with this amenity
    /// </summary>
    public virtual ICollection<PropertyTypeAmenity> PropertyTypeAmenities { get; set; } = new List<PropertyTypeAmenity>();
} 