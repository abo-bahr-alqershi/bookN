namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان العقار
/// Property entity
/// </summary>
public class Property : BaseEntity
{
    /// <summary>
    /// معرف المالك
    /// Owner identifier
    /// </summary>
    public Guid OwnerId { get; set; }
    
    /// <summary>
    /// معرف نوع العقار
    /// Property type identifier
    /// </summary>
    public Guid TypeId { get; set; }
    
    /// <summary>
    /// اسم العقار
    /// Property name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// عنوان العقار
    /// Property address
    /// </summary>
    public string Address { get; set; }
    
    /// <summary>
    /// المدينة
    /// City
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// خط العرض
    /// Latitude
    /// </summary>
    public decimal Latitude { get; set; }
    
    /// <summary>
    /// خط الطول
    /// Longitude
    /// </summary>
    public decimal Longitude { get; set; }
    
    /// <summary>
    /// تصنيف النجوم
    /// Star rating
    /// </summary>
    public int StarRating { get; set; }
    
    /// <summary>
    /// وصف العقار
    /// Property description
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// حالة الموافقة على العقار
    /// Property approval status
    /// </summary>
    public bool IsApproved { get; set; }
    
    /// <summary>
    /// تاريخ إنشاء العقار
    /// Property creation date
    /// </summary>
    public DateTime CreatedAt { get; set; }

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
    /// المالك المرتبط بالعقار
    /// Owner associated with the property
    /// </summary>
    public virtual User Owner { get; set; }
    
    /// <summary>
    /// نوع العقار المرتبط
    /// Property type associated
    /// </summary>
    public virtual PropertyType PropertyType { get; set; }
    
    /// <summary>
    /// الوحدات المرتبطة بالعقار
    /// Units associated with the property
    /// </summary>
    public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();
    
    /// <summary>
    /// الخدمات المرتبطة بالعقار
    /// Services associated with the property
    /// </summary>
    public virtual ICollection<PropertyService> Services { get; set; } = new List<PropertyService>();
    
    /// <summary>
    /// السياسات المرتبطة بالعقار
    /// Policies associated with the property
    /// </summary>
    public virtual ICollection<PropertyPolicy> Policies { get; set; } = new List<PropertyPolicy>();
    
    /// <summary>
    /// المراجعات المرتبطة بالعقار
    /// Reviews associated with the property
    /// </summary>
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    /// <summary>
    /// الموظفون المرتبطون بالعقار
    /// Staff associated with the property
    /// </summary>
    public virtual ICollection<Staff> Staff { get; set; } = new List<Staff>();
    
    /// <summary>
    /// الصور المرتبطة بالعقار
    /// Images associated with the property
    /// </summary>
    public virtual ICollection<PropertyImage> Images { get; set; } = new List<PropertyImage>();
    
    /// <summary>
    /// الوسائل المرتبطة بالعقار
    /// Amenities associated with the property
    /// </summary>
    public virtual ICollection<PropertyAmenity> Amenities { get; set; } = new List<PropertyAmenity>();

    /// <summary>
    /// البلاغات المرتبطة بالعقار
    /// Reports associated with the property
    /// </summary>
    public virtual ICollection<Report> Reports { get; set; } = new List<Report>();

} 