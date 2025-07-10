namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء مرفق جديد
/// Event for amenity creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء مرفق جديد في النظام
/// This event is triggered when a new amenity is created in the system
/// </remarks>
public interface IAmenityCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف المرفق الجديد
    /// ID of the new amenity
    /// </summary>
    Guid AmenityId { get; }
    
    /// <summary>
    /// معرف الإداري الذي أنشأ المرفق
    /// ID of the admin who created the amenity
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// اسم المرفق
    /// Amenity name
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// وصف المرفق
    /// Amenity description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// تاريخ إنشاء المرفق
    /// Amenity creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث مرفق
/// Event for amenity update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث مرفق موجود
/// This event is triggered when an existing amenity is updated
/// </remarks

/// <summary>
/// حدث تخصيص مرفق لنوع عقار
/// Event for assigning amenity to property type
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تخصيص مرفق لنوع عقار معين
/// This event is triggered when an amenity is assigned to a specific property type
/// </remarks>
public interface IAmenityAssignedToPropertyTypeEvent : IDomainEvent
{
    /// <summary>
    /// معرف وسيلة نوع العقار
    /// Property type amenity ID
    /// </summary>
    Guid PtaId { get; }
    
    /// <summary>
    /// معرف نوع العقار
    /// Property type ID
    /// </summary>
    Guid PropertyTypeId { get; }
    
    /// <summary>
    /// معرف المرفق
    /// Amenity ID
    /// </summary>
    Guid AmenityId { get; }
    
    /// <summary>
    /// معرف الإداري الذي قام بالتخصيص
    /// ID of the admin who made the assignment
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// هل هو مرفق افتراضي
    /// Whether it's a default amenity
    /// </summary>
    bool IsDefault { get; }
    
    /// <summary>
    /// تاريخ التخصيص
    /// Assignment date
    /// </summary>
    DateTime AssignedAt { get; }
}

/// <summary>
/// حدث تحديث حالة مرفق العقار
/// Event for property amenity update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث حالة مرفق في عقار محدد
/// This event is triggered when property amenity status is updated
/// </remarks>
public interface IPropertyAmenityUpdatedEvent : IDomainEvent
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
    /// معرف المرفق
    /// Amenity ID
    /// </summary>
    Guid AmenityId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي قام بالتحديث
    /// ID of the user who made the update
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// حالة التوفر الجديدة (إن تم تحديثها)
    /// New availability status (if updated)
    /// </summary>
    bool? NewIsAvailable { get; }
    
    /// <summary>
    /// التكلفة الإضافية الجديدة (إن تم تحديثها)
    /// New extra cost (if updated)
    /// </summary>
    string? NewExtraCost { get; }
}
