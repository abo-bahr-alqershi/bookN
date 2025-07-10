using System;
using System.Collections.Generic;

namespace YemenBooking.Core.Entities;

/// <summary>
/// كيان يمثل بلاغ قام به مستخدم عن عقار أو مستخدم آخر
/// Represents a report filed by a user about a property or another user
/// </summary>
public class Report : BaseEntity
{
    /// <summary>
    /// معرف المستخدم المبلغ
    /// Reporter user identifier
    /// </summary>
    public Guid ReporterUserId { get; set; }

    /// <summary>
    /// معرف المستخدم المبلغ عنه (اختياري)
    /// Reported user identifier (optional)
    /// </summary>
    public Guid? ReportedUserId { get; set; }

    /// <summary>
    /// معرف العقار المبلغ عنه (اختياري)
    /// Reported property identifier (optional)
    /// </summary>
    public Guid? ReportedPropertyId { get; set; }

    /// <summary>
    /// سبب البلاغ
    /// Reason for the report
    /// </summary>
    public string Reason { get; set; } = string.Empty;

    /// <summary>
    /// تفاصيل البلاغ
    /// Detailed description of the report
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// تنقل الخصائص المفصلة لاحقًا
    /// Navigation properties for EF Core
    /// </summary>
    public User ReporterUser { get; set; } = null!;
    public User? ReportedUser { get; set; }
    public Property? ReportedProperty { get; set; }
} 