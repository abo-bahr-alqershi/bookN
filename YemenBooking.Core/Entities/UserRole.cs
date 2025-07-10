namespace YemenBooking.Core.Entities;

using System;

/// <summary>
/// كيان دور المستخدم
/// User Role entity
/// </summary>
public class UserRole : BaseEntity
{
    /// <summary>
    /// معرف المستخدم
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// معرف الدور
    /// Role identifier
    /// </summary>
    public Guid RoleId { get; set; }
    
    /// <summary>
    /// تاريخ التخصيص
    /// Assignment date
    /// </summary>
    public DateTime AssignedAt { get; set; }
    
    /// <summary>
    /// المستخدم المرتبط بالدور
    /// User associated with the role
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// الدور المرتبط بالمستخدم
    /// Role associated with the user
    /// </summary>
    public virtual Role Role { get; set; }
} 