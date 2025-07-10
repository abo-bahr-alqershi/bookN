using System;
using MediatR;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Application.Queries.Reports;

/// <summary>
/// استعلام للحصول على جميع البلاغات
/// Query to get all reports with optional filters
/// </summary>
public class GetAllReportsQuery : IRequest<PaginatedResult<ReportDto>>
{
    /// <summary>
    /// رقم الصفحة
    /// Page number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// حجم الصفحة
    /// Page size
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// معرف المستخدم المبلغ
    /// Reporter user identifier (optional)
    /// </summary>
    public Guid? ReporterUserId { get; set; }

    /// <summary>
    /// معرف المستخدم المبلغ عنه
    /// Reported user identifier (optional)
    /// </summary>
    public Guid? ReportedUserId { get; set; }

    /// <summary>
    /// معرف العقار المبلغ عنه
    /// Reported property identifier (optional)
    /// </summary>
    public Guid? ReportedPropertyId { get; set; }
} 