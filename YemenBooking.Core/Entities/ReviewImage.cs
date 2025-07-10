namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان صورة التقييم
/// Review Image entity
/// </summary>
public class ReviewImage : BaseEntity
{
    /// <summary>
    /// معرف التقييم المرتبط
    /// Identifier of the related review
    /// </summary>
    public Guid ReviewId { get; set; }

    /// <summary>
    /// اسم الملف
    /// File name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// مسار الصورة
    /// Image URL or path
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// حجم الملف بالبايت
    /// File size in bytes
    /// </summary>
    public long SizeBytes { get; set; }

    /// <summary>
    /// نوع المحتوى
    /// Content type or file type
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// فئة الصورة
    /// Image category
    /// </summary>
    public ImageCategory Category { get; set; }

    /// <summary>
    /// تعليق توضيحي للصورة
    /// Image caption
    /// </summary>
    public string Caption { get; set; } = string.Empty;

    /// <summary>
    /// نص بديل للصورة
    /// Alt text for the image
    /// </summary>
    public string AltText { get; set; } = string.Empty;

    /// <summary>
    /// وسوم الصورة (JSON)
    /// Tags of the image in JSON
    /// </summary>
    public string Tags { get; set; } = string.Empty;

    /// <summary>
    /// هل هي الصورة الرئيسية
    /// Is main image
    /// </summary>
    public bool IsMain { get; set; } = false;

    /// <summary>
    /// ترتيب العرض
    /// Display order
    /// </summary>
    public int DisplayOrder { get; set; } = 0;

    /// <summary>
    /// تاريخ الرفع
    /// Upload date
    /// </summary>
    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// حالة الموافقة
    /// Approval status of the image
    /// </summary>
    public ImageStatus Status { get; set; } = ImageStatus.Pending;

    /// <summary>
    /// الكيان التابع للتقييم
    /// Navigation property to the review
    /// </summary>
    public virtual Review Review { get; set; }
} 