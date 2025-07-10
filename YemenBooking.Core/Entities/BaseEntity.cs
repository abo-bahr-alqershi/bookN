using YemenBooking.Core.Interfaces;

namespace YemenBooking.Core.Entities;

/// <summary>
/// الكيان الأساسي الذي ترث منه جميع الكيانات في النظام
/// Base entity class that all entities inherit from
/// </summary>
public abstract class BaseEntity : IBaseEntity
{
    /// <summary>
    /// المعرف الفريد للكيان
    /// Unique identifier for the entity
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// المستخدم اللذي إنشاء الكيان
    /// Creation date of the entity
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// تاريخ إنشاء الكيان
    /// Creation date of the entity
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// المستخدم اللذي حدث الكيان
    /// Creation date of the entity
    /// </summary>
    public Guid? UpdatedBy { get; set; }

    /// <summary>
    /// تاريخ آخر تحديث للكيان
    /// Last update date of the entity
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    /// <summary>
    /// حالة نشاط الكيان
    /// Activity status of the entity
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// Indicates if the entity is soft-deleted.
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// المستخدم اللذي حذف الكيان
    /// Creation date of the entity
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// Date when the entity is soft-deleted.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}