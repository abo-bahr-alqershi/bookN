using System;
using YemenBooking.Core.Enums;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث تحديث بيانات صورة في معرض العقار أو الوحدة
/// Event for property or unit image update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث بيانات صورة في معرض العقار أو الوحدة
/// This event is triggered when a property's or unit's gallery image data is updated
/// </remarks>
public interface IPropertyImageUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الصورة المحدثة
    /// ID of the updated image
    /// </summary>
    Guid ImageId { get; }

    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }

    /// <summary>
    /// الرابط الجديد أو المسار (إن تم تحديثه)
    /// New URL or path (if updated)
    /// </summary>
    string? NewUrl { get; }

    /// <summary>
    /// التعليق التوضيحي الجديد (إن تم تحديثه)
    /// New caption (if updated)
    /// </summary>
    string? NewCaption { get; }

    /// <summary>
    /// النص البديل الجديد (إن تم تحديثه)
    /// New alt text (if updated)
    /// </summary>
    string? NewAltText { get; }

    /// <summary>
    /// فئة الصورة الجديدة (إن تم تحديثها)
    /// New image category (if updated)
    /// </summary>
    ImageCategory? NewCategory { get; }

    /// <summary>
    /// تحديد إذا كانت الصورة رئيسية (إن تم تحديثه)
    /// New is main flag (if updated)
    /// </summary>
    bool? NewIsMain { get; }
} 