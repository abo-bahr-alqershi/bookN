namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إضافة موظف جديد
/// Event for staff addition
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إضافة موظف جديد لعقار
/// This event is triggered when a new staff member is added to a property
/// </remarks>
public interface IStaffAddedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الموظف الجديد
    /// ID of the new staff member
    /// </summary>
    Guid StaffId { get; }
    
    /// <summary>
    /// معرف المستخدم المعين كموظف
    /// ID of the user assigned as staff
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المدير الذي قام بالتعيين
    /// ID of the manager who made the assignment
    /// </summary>
    Guid AssignedBy { get; }
    
    /// <summary>
    /// منصب الموظف
    /// Staff position
    /// </summary>
    string Position { get; }
    
    /// <summary>
    /// الصلاحيات الممنوحة
    /// Granted permissions
    /// </summary>
    string Permissions { get; }
    
    /// <summary>
    /// تاريخ التعيين
    /// Assignment date
    /// </summary>
    DateTime AssignedAt { get; }
}

/// <summary>
/// حدث تحديث بيانات موظف
/// Event for staff update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث بيانات موظف موجود
/// This event is triggered when existing staff data is updated
/// </remarks>
public interface IStaffUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الموظف المحدث
    /// ID of the updated staff member
    /// </summary>
    Guid StaffId { get; }
    
    /// <summary>
    /// معرف المستخدم الموظف
    /// Staff user ID
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المدير الذي قام بالتحديث
    /// ID of the manager who made the update
    /// </summary>
    Guid UpdatedBy { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// المنصب الجديد (إن تم تحديثه)
    /// New position (if updated)
    /// </summary>
    string? NewPosition { get; }
    
    /// <summary>
    /// الصلاحيات الجديدة (إن تم تحديثها)
    /// New permissions (if updated)
    /// </summary>
    string? NewPermissions { get; }
}

/// <summary>
/// حدث إلغاء تعيين موظف
/// Event for staff removal
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إلغاء تعيين موظف من عقار
/// This event is triggered when a staff member is removed from a property
/// </remarks>
public interface IStaffRemovedEvent : IDomainEvent
{
    /// <summary>
    /// معرف الموظف المحذوف
    /// ID of the removed staff member
    /// </summary>
    Guid StaffId { get; }
    
    /// <summary>
    /// معرف المستخدم الموظف
    /// Staff user ID
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المدير الذي قام بالإزالة
    /// ID of the manager who made the removal
    /// </summary>
    Guid RemovedBy { get; }
    
    /// <summary>
    /// منصب الموظف المحذوف
    /// Position of the removed staff
    /// </summary>
    string Position { get; }
    
    /// <summary>
    /// سبب الإزالة
    /// Removal reason
    /// </summary>
    string RemovalReason { get; }
    
    /// <summary>
    /// تاريخ الإزالة
    /// Removal date
    /// </summary>
    DateTime RemovedAt { get; }
}
