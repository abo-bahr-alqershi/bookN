namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان صورة العقار
/// Property Image entity
/// </summary>
public class PropertyImage : BaseEntity
{
    /// <summary>
    /// معرف العقار (قابل للتمرير إلى NULL)
    /// Property identifier (nullable)
    /// </summary>
    public Guid? PropertyId { get; set; }
    
    /// <summary>
    /// معرف الوحدة (قابل للتمرير إلى NULL)
    /// Unit identifier (nullable)
    /// </summary>
    public Guid? UnitId { get; set; }
    
    /// <summary>
    /// اسم الصورة
    /// Image name
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// رابط الصورة
    /// Image URL
    /// </summary>
    public string Url { get; set; }
    
    /// <summary>
    /// حجم الصورة بالبايت
    /// Image size in bytes
    /// </summary>
    public long SizeBytes { get; set; }
    
    /// <summary>
    /// نوع الصورة
    /// Image type
    /// </summary>
    public string Type { get; set; }
    
    /// <summary>
    /// فئة الصورة
    /// Image category
    /// </summary>
    public ImageCategory Category { get; set; }
    
    /// <summary>
    /// تسمية توضيحية للصورة
    /// Image caption
    /// </summary>
    public string Caption { get; set; }
    
    /// <summary>
    /// نص بديل للصورة
    /// Image alt text
    /// </summary>
    public string AltText { get; set; }
    
    /// <summary>
    /// وسوم الصورة (JSON)
    /// Image tags (JSON)
    /// </summary>
    public string Tags { get; set; }
    
    /// <summary>
    /// هل هي الصورة الرئيسية
    /// Is main image
    /// </summary>
    public bool IsMain { get; set; }
    
    /// <summary>
    /// ترتيب العرض
    /// Sort order
    /// </summary>
    public int SortOrder { get; set; }
    
    /// <summary>
    /// عدد المشاهدات
    /// Number of views
    /// </summary>
    public int Views { get; set; }
    
    /// <summary>
    /// عدد التنزيلات
    /// Number of downloads
    /// </summary>
    public int Downloads { get; set; }
    
    /// <summary>
    /// تاريخ الرفع
    /// Upload date
    /// </summary>
    public DateTime UploadedAt { get; set; }
    
    /// <summary>
    /// ترتيب العرض
    /// Display order
    /// </summary>
    public int DisplayOrder { get; set; }
    
    /// <summary>
    /// حالة الصورة
    /// Image status
    /// </summary>
    public ImageStatus Status { get; set; }
    
    /// <summary>
    /// هل هي الصورة الرئيسية
    /// Is main image
    /// </summary>
    public bool IsMainImage { get; set; }
    
    /// <summary>
    /// العقار المرتبط بالصورة (قابل للتمرير إلى NULL)
    /// Property associated with the image (nullable)
    /// </summary>
    public virtual Property? Property { get; set; }
    
    /// <summary>
    /// الوحدة المرتبطة بالصورة (قابل للتمرير إلى NULL)
    /// Unit associated with the image (nullable)
    /// </summary>
    public virtual Unit? Unit { get; set; }
}