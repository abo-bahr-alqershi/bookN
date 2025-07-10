namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء نوع عقار جديد
/// Event for property type creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء نوع عقار جديد في النظام
/// This event is triggered when a new property type is created in the system
/// </remarks>
public interface IPropertyTypeCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع العقار الجديد
    /// ID of the new property type
    /// </summary>
    Guid TypeId { get; }
    
    /// <summary>
    /// معرف الإداري الذي أنشأ النوع
    /// ID of the admin who created the type
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// اسم نوع العقار
    /// Property type name
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// وصف نوع العقار
    /// Property type description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// المرافق الافتراضية
    /// Default amenities
    /// </summary>
    string DefaultAmenities { get; }
    
    /// <summary>
    /// تاريخ الإنشاء
    /// Creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث نوع عقار
/// Event for property type update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث نوع عقار موجود
/// This event is triggered when an existing property type is updated
/// </remarks>
public interface IPropertyTypeUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع العقار المحدث
    /// ID of the updated property type
    /// </summary>
    Guid TypeId { get; }
    
    /// <summary>
    /// معرف الإداري الذي حدث النوع
    /// ID of the admin who updated the type
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
    /// المرافق الافتراضية الجديدة (إن تم تحديثها)
    /// New default amenities (if updated)
    /// </summary>
    string? NewDefaultAmenities { get; }
}

/// <summary>
/// حدث إنشاء نوع وحدة جديد
/// Event for unit type creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء نوع وحدة جديد لنوع عقار
/// This event is triggered when a new unit type is created for a property type
/// </remarks>
public interface IUnitTypeCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع الوحدة الجديد
    /// ID of the new unit type
    /// </summary>
    Guid UnitTypeId { get; }
    
    /// <summary>
    /// معرف نوع العقار
    /// Property type ID
    /// </summary>
    Guid PropertyTypeId { get; }
    
    /// <summary>
    /// معرف الإداري الذي أنشأ النوع
    /// ID of the admin who created the type
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// اسم نوع الوحدة
    /// Unit type name
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// الحد الأقصى للسعة
    /// Maximum capacity
    /// </summary>
    int MaxCapacity { get; }
    
    /// <summary>
    /// المرافق الافتراضية
    /// Default amenities
    /// </summary>
    string DefaultAmenities { get; }
    
    /// <summary>
    /// السعر الأساسي لليلة الواحدة
    /// Base price per night
    /// </summary>
    decimal BasePricePerNight { get; }
    
    /// <summary>
    /// تاريخ الإنشاء
    /// Creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث نوع وحدة
/// Event for unit type update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث نوع وحدة موجود
/// This event is triggered when an existing unit type is updated
/// </remarks>
public interface IUnitTypeUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف نوع الوحدة المحدث
    /// ID of the updated unit type
    /// </summary>
    Guid UnitTypeId { get; }
    
    /// <summary>
    /// معرف نوع العقار
    /// Property type ID
    /// </summary>
    Guid PropertyTypeId { get; }
    
    /// <summary>
    /// معرف الإداري الذي حدث النوع
    /// ID of the admin who updated the type
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
    /// السعة القصوى الجديدة (إن تم تحديثها)
    /// New maximum capacity (if updated)
    /// </summary>
    int? NewMaxCapacity { get; }
    
    /// <summary>
    /// المرافق الافتراضية الجديدة (إن تم تحديثها)
    /// New default amenities (if updated)
    /// </summary>
    string? NewDefaultAmenities { get; }
    
    /// <summary>
    /// السعر الأساسي الجديد (إن تم تحديثه)
    /// New base price (if updated)
    /// </summary>
    decimal? NewBasePricePerNight { get; }
}
