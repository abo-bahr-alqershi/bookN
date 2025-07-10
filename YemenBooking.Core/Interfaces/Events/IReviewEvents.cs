namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء تقييم جديد
/// Event for review creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء تقييم جديد لحجز
/// This event is triggered when a new review is created for a booking
/// </remarks>
public interface IReviewCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف التقييم الجديد
    /// ID of the new review
    /// </summary>
    Guid ReviewId { get; }
    
    /// <summary>
    /// معرف الحجز المُقيم
    /// ID of the reviewed booking
    /// </summary>
    Guid BookingId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أنشأ التقييم
    /// ID of the user who created the review
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// تقييم النظافة
    /// Cleanliness rating
    /// </summary>
    int Cleanliness { get; }
    
    /// <summary>
    /// تقييم الخدمة
    /// Service rating
    /// </summary>
    int Service { get; }
    
    /// <summary>
    /// تقييم الموقع
    /// Location rating
    /// </summary>
    int Location { get; }
    
    /// <summary>
    /// تقييم القيمة
    /// Value rating
    /// </summary>
    int Value { get; }
    
    /// <summary>
    /// تعليق التقييم
    /// Review comment
    /// </summary>
    string Comment { get; }
    
    /// <summary>
    /// تاريخ إنشاء التقييم
    /// Review creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث تقييم
/// Event for review update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث تقييم موجود
/// This event is triggered when an existing review is updated
/// </remarks>
public interface IReviewUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف التقييم المحدث
    /// ID of the updated review
    /// </summary>
    Guid ReviewId { get; }
    
    /// <summary>
    /// معرف الحجز
    /// Booking ID
    /// </summary>
    Guid BookingId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حدث التقييم
    /// ID of the user who updated the review
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// تقييم النظافة الجديد (إن تم تحديثه)
    /// New cleanliness rating (if updated)
    /// </summary>
    int? NewCleanliness { get; }
    
    /// <summary>
    /// تقييم الخدمة الجديد (إن تم تحديثه)
    /// New service rating (if updated)
    /// </summary>
    int? NewService { get; }
    
    /// <summary>
    /// تقييم الموقع الجديد (إن تم تحديثه)
    /// New location rating (if updated)
    /// </summary>
    int? NewLocation { get; }
    
    /// <summary>
    /// تقييم القيمة الجديد (إن تم تحديثه)
    /// New value rating (if updated)
    /// </summary>
    int? NewValue { get; }
    
    /// <summary>
    /// التعليق الجديد (إن تم تحديثه)
    /// New comment (if updated)
    /// </summary>
    string? NewComment { get; }
}

/// <summary>
/// حدث حذف تقييم
/// Event for review deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف تقييم
/// This event is triggered when a review is deleted
/// </remarks>
public interface IReviewDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف التقييم المحذوف
    /// ID of the deleted review
    /// </summary>
    Guid ReviewId { get; }
    
    /// <summary>
    /// معرف الحجز
    /// Booking ID
    /// </summary>
    Guid BookingId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حذف التقييم
    /// ID of the user who deleted the review
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// سبب الحذف
    /// Deletion reason
    /// </summary>
    string DeletionReason { get; }
    
    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }
}
