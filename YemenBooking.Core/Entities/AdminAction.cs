namespace YemenBooking.Core.Entities;

using System;
using YemenBooking.Core.Enums;

/// <summary>
/// كيان إجراء الإدارة
/// Admin Action entity
/// </summary>
public class AdminAction : BaseEntity
{
    /// <summary>
    /// معرف المدير
    /// Admin identifier
    /// </summary>
    public Guid AdminId { get; set; }
    
    /// <summary>
    /// معرف الهدف
    /// Target identifier
    /// </summary>
    public Guid TargetId { get; set; }
    
    /// <summary>
    /// نوع الهدف (property, user, booking)
    /// Target type (property, user, booking)
    /// </summary>
    public TargetType TargetType { get; set; }
    
    /// <summary>
    /// نوع الإجراء (create, update, delete, approve)
    /// Action type (create, update, delete, approve)
    /// </summary>
    public ActionType ActionType { get; set; }
    
    /// <summary>
    /// الطابع الزمني للإجراء
    /// Action timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    /// التغييرات (JSON)
    /// Changes (JSON)
    /// </summary>
    public string Changes { get; set; }
    
    /// <summary>
    /// المدير المرتبط بالإجراء
    /// Admin associated with the action
    /// </summary>
    public virtual User Admin { get; set; }
} 