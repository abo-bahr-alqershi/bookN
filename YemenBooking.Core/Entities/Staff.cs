namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان الموظف
/// Staff entity
/// </summary>
public class Staff : BaseEntity
{
    /// <summary>
    /// معرف المستخدم
    /// User identifier
    /// </summary>
    public Guid UserId { get; set; }
    
    /// <summary>
    /// معرف العقار
    /// Property identifier
    /// </summary>
    public Guid PropertyId { get; set; }
    
    /// <summary>
    /// منصب الموظف (مدير، موظف استقبال، نظافة)
    /// Staff position (Manager, Receptionist, Housekeeping)
    /// </summary>
    public StaffPosition Position { get; set; }
    
    /// <summary>
    /// الصلاحيات (JSON)
    /// Permissions (JSON)
    /// </summary>
    public string Permissions { get; set; }
    
    /// <summary>
    /// المستخدم المرتبط بالموظف
    /// User associated with the staff
    /// </summary>
    public virtual User User { get; set; }
    
    /// <summary>
    /// العقار المرتبط بالموظف
    /// Property associated with the staff
    /// </summary>
    public virtual Property Property { get; set; }
}