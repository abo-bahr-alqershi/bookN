namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء سياسة عقار جديدة
/// Event for property policy creation
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند إنشاء سياسة جديدة لعقار
/// This event is triggered when a new policy is created for a property
/// </remarks>
public interface IPropertyPolicyCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف السياسة الجديدة
    /// ID of the new policy
    /// </summary>
    Guid PolicyId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي أنشأ السياسة
    /// ID of the user who created the policy
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// نوع السياسة
    /// Policy type
    /// </summary>
    string PolicyType { get; }
    
    /// <summary>
    /// وصف السياسة
    /// Policy description
    /// </summary>
    string Description { get; }
    
    /// <summary>
    /// قواعد السياسة
    /// Policy rules
    /// </summary>
    string Rules { get; }
    
    /// <summary>
    /// تاريخ إنشاء السياسة
    /// Policy creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث سياسة عقار
/// Event for property policy update
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند تحديث سياسة موجودة
/// This event is triggered when an existing policy is updated
/// </remarks>
public interface IPropertyPolicyUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف السياسة المحدثة
    /// ID of the updated policy
    /// </summary>
    Guid PolicyId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حدث السياسة
    /// ID of the user who updated the policy
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }
    
    /// <summary>
    /// الوصف الجديد (إن تم تحديثه)
    /// New description (if updated)
    /// </summary>
    string? NewDescription { get; }
    
    /// <summary>
    /// القواعد الجديدة (إن تم تحديثها)
    /// New rules (if updated)
    /// </summary>
    string? NewRules { get; }
}

/// <summary>
/// حدث حذف سياسة
/// Event for policy deletion
/// </summary>
/// <remarks>
/// يتم إثارة هذا الحدث عند حذف سياسة
/// This event is triggered when a policy is deleted
/// </remarks>
public interface IPolicyDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف السياسة المحذوفة
    /// ID of the deleted policy
    /// </summary>
    Guid PolicyId { get; }
    
    /// <summary>
    /// معرف العقار
    /// Property ID
    /// </summary>
    Guid PropertyId { get; }
    
    /// <summary>
    /// معرف المستخدم الذي حذف السياسة
    /// ID of the user who deleted the policy
    /// </summary>
    new Guid UserId { get; }
    
    /// <summary>
    /// نوع السياسة المحذوفة
    /// Type of the deleted policy
    /// </summary>
    string PolicyType { get; }
    
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
