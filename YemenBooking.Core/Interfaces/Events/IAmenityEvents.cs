using YemenBooking.Core.ValueObjects;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إضافة مرفق إلى العقار
/// Event for adding amenity to property
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إضافة مرفق جديد إلى عقار
/// This event is triggered when a new amenity is added to a property
/// </remarks>
public interface IAmenityAddedToPropertyEvent : IDomainEvent
{
    /// <summary>
    /// معرف وسيلة العقار
    /// Property amenity ID
    /// </summary>
    Guid PaId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف وسيلة نوع العقار
    /// Property type amenity ID
    /// </summary>
    Guid PtaId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أضاف المرفق
    /// ID of the user who added the amenity
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// هل المرفق متاح
    /// Whether amenity is available
    /// </summary>
    bool IsAvailable { get; }
    
    /// <summary>
    /// التكلفة الإضافية للمرفق
    /// Extra cost for the amenity
    /// </summary>
    Money ExtraCost { get; }
    
    /// <summary>
    /// تاريخ إضافة المرفق
    /// Date when amenity was added
    /// </summary>
    DateTime AddedAt { get; }
}

/// <summary>
/// حدث تحديث مرفق العقار
/// Event for updating property amenity
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث تفاصيل مرفق في عقار
/// This event is triggered when property amenity details are updated
/// </remarks>
public interface IAmenityUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف وسيلة العقار
    /// Property amenity ID
    /// </summary>
    Guid PaId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حدث المرفق
    /// ID of the user who updated the amenity
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// الحالة الجديدة للتوفر (إن تم تحديثها)
    /// New availability status (if updated)
    /// </summary>
    bool? NewIsAvailable { get; }
    
    /// <summary>
    /// التكلفة الإضافية الجديدة (إن تم تحديثها)
    /// New extra cost (if updated)
    /// </summary>
    Money? NewExtraCost { get; }
}

/// <summary>
/// حدث إزالة مرفق من العقار
/// Event for removing amenity from property
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إزالة مرفق من عقار
/// This event is triggered when an amenity is removed from a property
/// </remarks>
public interface IAmenityRemovedFromPropertyEvent : IDomainEvent
{
    /// <summary>
    /// معرف وسيلة العقار المحذوفة
    /// ID of the removed property amenity
    /// </summary>
    Guid PaId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف وسيلة نوع العقار
    /// Property type amenity ID
    /// </summary>
    Guid PtaId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أزال المرفق
    /// ID of the user who removed the amenity
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// سبب الإزالة
    /// Removal reason
    /// </summary>
    string RemovalReason { get; }
    
    /// <summary>
    /// تاريخ إزالة المرفق
    /// Date when amenity was removed
    /// </summary>
    DateTime RemovedAt { get; }
}
