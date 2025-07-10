using System;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث حذف صورة من معرض العقار أو الوحدة
/// Event for property or unit image deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف صورة من معرض العقار أو الوحدة
/// This event is triggered when an image is removed from a property's or unit's gallery
/// </remarks>
public interface IPropertyImageDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الصورة المحذوفة
    /// ID of the deleted image
    /// </summary>
    Guid ImageId { get; }

    /// <summary>
    /// معرف المستخدم الذي حذف الصورة
    /// ID of the user who deleted the image
    /// </summary>
    new Guid UserId { get; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }
} 