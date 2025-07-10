using System;
using YemenBooking.Core.Interfaces.Events;

namespace YemenBooking.Core.Interfaces.Events;

/// <summary>
/// حدث إنشاء بلاغ جديد
/// Event for report creation
/// </summary>
public interface IReportCreatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف البلاغ
    /// Report identifier
    /// </summary>
    Guid ReportId { get; }

    /// <summary>
    /// معرف المستخدم المبلغ
    /// Reporter user identifier
    /// </summary>
    Guid ReporterUserId { get; }

    /// <summary>
    /// معرف المستخدم المبلغ عنه (اختياري)
    /// Reported user identifier (optional)
    /// </summary>
    Guid? ReportedUserId { get; }

    /// <summary>
    /// معرف العقار المبلغ عنه (اختياري)
    /// Reported property identifier (optional)
    /// </summary>
    Guid? ReportedPropertyId { get; }

    /// <summary>
    /// سبب البلاغ
    /// Reason for the report
    /// </summary>
    string Reason { get; }

    /// <summary>
    /// تفاصيل البلاغ
    /// Description of the report
    /// </summary>
    string Description { get; }

    /// <summary>
    /// تاريخ إنشاء البلاغ
    /// Report creation date
    /// </summary>
    DateTime CreatedAt { get; }
}

/// <summary>
/// حدث تحديث بلاغ
/// Event for report update
/// </summary>
public interface IReportUpdatedEvent : IDomainEvent
{
    /// <summary>
    /// معرف البلاغ المحدث
    /// Updated report identifier
    /// </summary>
    Guid ReportId { get; }

    /// <summary>
    /// الحقول المحدثة
    /// Updated fields
    /// </summary>
    string[] UpdatedFields { get; }

    /// <summary>
    /// السبب الجديد (إن تم تحديثه)
    /// New reason (if updated)
    /// </summary>
    string? NewReason { get; }

    /// <summary>
    /// التفاصيل الجديدة (إن تم تحديثها)
    /// New description (if updated)
    /// </summary>
    string? NewDescription { get; }
}

/// <summary>
/// حدث حذف بلاغ
/// Event for report deletion
/// </summary>
public interface IReportDeletedEvent : IDomainEvent
{
    /// <summary>
    /// معرف البلاغ المحذوف
    /// Deleted report identifier
    /// </summary>
    Guid ReportId { get; }

    /// <summary>
    /// سبب الحذف (اختياري)
    /// Deletion reason (optional)
    /// </summary>
    string? DeletionReason { get; }

    /// <summary>
    /// تاريخ الحذف
    /// Deletion date
    /// </summary>
    DateTime DeletedAt { get; }
} 