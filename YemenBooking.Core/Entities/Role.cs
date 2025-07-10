namespace YemenBooking.Core.Entities;

using System;
using System.Collections.Generic;

/// <summary>
/// كيان الدور
/// Role entity
/// </summary>
public class Role : BaseEntity
{
    /// <summary>
    /// اسم الدور (admin, owner, manager, customer)
    /// Role name (admin, owner, manager, customer)
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// المستخدمون المرتبطون بهذا الدور
    /// Users associated with this role
    /// </summary>
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
} 