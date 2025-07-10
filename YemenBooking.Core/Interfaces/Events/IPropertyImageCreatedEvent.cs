using System;
using YemenBooking.Core.Enums;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء صورة جديدة في معرض العقار أو الوحدة
/// Event for property or unit image creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إضافة صورة جديدة إلى معرض العقار أو الوحدة
/// This event is triggered when a new image is added to a property's or unit's gallery
/// </remarks>
public interface IPropertyImageCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الصورة الجديدة
    /// ID of the new image
    /// </summary>
    Guid ImageId { get; }

    /// <summary>
    /// معرف العقار (إن وجد)
    /// Property ID (if any)
    /// </summary>
    Guid? PropertyId { get; }

    /// <summary>
    /// معرف الوحدة (إن وجد)
    /// Unit ID (if any)
    /// </summary>
    Guid? UnitId { get; }

    /// <summary>
    /// رابط أو مسار الصورة
    /// Image URL or path
    /// </summary>
    string Url { get; }

    /// <summary>
    /// تعليق توضيحي للصورة
    /// Image caption
    /// </summary>
    string Caption { get; }

    /// <summary>
    /// النص البديل للصورة
    /// Alt text for the image
    /// </summary>
    string AltText { get; }

    /// <summary>
    /// فئة الصورة
    /// Image category
    /// </summary>
    ImageCategory Category { get; }

    /// <summary>
    /// تحديد إذا كانت الصورة رئيسية
    /// Indicates whether the image is the main one
    /// </summary>
    bool IsMain { get; }

    /// <summary>
    /// تاريخ إنشاء الصورة
    /// Image creation date
    /// </summary>
    DateTime CreatedAt { get; }
} 