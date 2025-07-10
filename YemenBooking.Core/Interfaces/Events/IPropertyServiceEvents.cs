using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء خدمة عقار جديدة
/// Event for property service creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء خدمة جديدة للعقار
/// This event is triggered when a new service is created for a property
/// </remarks>
public interface IPropertyServiceCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الخدمة الجديدة
    /// ID of the new service
    /// </summary>
    Guid ServiceId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أنشأ الخدمة
    /// ID of the user who created the service
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// اسم الخدمة
    /// Service name
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// وصف الخدمة
    /// Service description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// سعر الخدمة
    /// Service price
    /// </summary>
    Money Price { get; }
    
    /// <summary>
    /// نموذج التسعير
    /// Pricing model
    /// </summary>
    string PricingModel { get; }
    
    /// <summary>
    /// هل الخدمة متاحة
    /// Whether service is available
    /// </summary>
    bool IsAvailable { get; }
    
    /// <summary>
    /// تاريخ إنشاء الخدمة
    /// Service creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث خدمة العقار
/// Event for property service update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث خدمة موجودة للعقار
/// This event is triggered when an existing property service is updated
/// </remarks>
public interface IPropertyServiceUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الخدمة المحدثة
    /// ID of the updated service
    /// </summary>
    Guid ServiceId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حدث الخدمة
    /// ID of the user who updated the service
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// الاسم الجديد (إن تم تحديثه)
    /// New name (if updated)
    /// </summary>
    string? NewName { get; }
    
    /// <summary>
    /// الوصف الجديد (إن تم تحديثه)
    /// New description (if updated)
    /// </summary>
    string? NewDescription { get; }
    
    /// <summary>
    /// السعر الجديد (إن تم تحديثه)
    /// New price (if updated)
    /// </summary>
    Money? NewPrice { get; }
    
    /// <summary>
    /// نموذج التسعير الجديد (إن تم تحديثه)
    /// New pricing model (if updated)
    /// </summary>
    string? NewPricingModel { get; }
    
    /// <summary>
    /// حالة التوفر الجديدة (إن تم تحديثها)
    /// New availability status (if updated)
    /// </summary>
    bool? NewIsAvailable { get; }
}
