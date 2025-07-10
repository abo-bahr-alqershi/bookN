using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.Reports;
using YemenBooking.Application.Exceptions;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.Reports
{
    /// <summary>
    /// معالج استعلام الحصول على جميع البلاغات مع الترميز
    /// Handles GetAllReportsQuery and returns paginated reports
    /// </summary>
    public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, PaginatedResult<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly ILogger<GetAllReportsQueryHandler> _logger;

        public GetAllReportsQueryHandler(
            IReportRepository reportRepository,
            ILogger<GetAllReportsQueryHandler> logger)
        {
            _reportRepository = reportRepository;
            _logger = logger;
        }

        public async Task<PaginatedResult<ReportDto>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء معالجة استعلام GetAllReports: Page={Page}, Size={Size}", request.PageNumber, request.PageSize);

            if (request.PageNumber <= 0 || request.PageSize <= 0)
                throw new BusinessRuleException("InvalidPagination", "رقم الصفحة وحجم الصفحة يجب أن يكونا أكبر من صفر");

            // جلب البيانات مع الفلاتر
            var allReports = await _reportRepository.GetReportsAsync(
                request.ReporterUserId,
                request.ReportedUserId,
                request.ReportedPropertyId,
                cancellationToken);

            var totalCount = allReports.Count();
            var items = allReports
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(r => new ReportDto
                {
                    Id = r.Id,
                    ReporterUserId = r.ReporterUserId,
                    ReportedUserId = r.ReportedUserId,
                    ReportedPropertyId = r.ReportedPropertyId,
                    Reason = r.Reason,
                    CreatedAt = r.CreatedAt
                })
                .ToList();

            return PaginatedResult<ReportDto>.Create(items, request.PageNumber, request.PageSize, totalCount);
        }
    }
} 