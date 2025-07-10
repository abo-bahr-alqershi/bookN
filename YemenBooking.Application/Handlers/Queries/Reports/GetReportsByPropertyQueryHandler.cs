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
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Application.Handlers.Queries.Reports
{
    /// <summary>
    /// معالج استعلام الحصول على البلاغات المتعلقة بعقار معين
    /// Handles GetReportsByPropertyQuery and returns paginated reports for a property
    /// </summary>
    public class GetReportsByPropertyQueryHandler : IRequestHandler<GetReportsByPropertyQuery, PaginatedResult<ReportDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetReportsByPropertyQueryHandler> _logger;

        public GetReportsByPropertyQueryHandler(
            IReportRepository reportRepository,
            ICurrentUserService currentUserService,
            ILogger<GetReportsByPropertyQueryHandler> logger)
        {
            _reportRepository = reportRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<PaginatedResult<ReportDto>> Handle(GetReportsByPropertyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء جلب بلاغات العقار: {PropertyId}, Page={Page}, Size={Size}", request.PropertyId, request.PageNumber, request.PageSize);

            if (request.PropertyId == Guid.Empty)
                throw new BusinessRuleException("InvalidPropertyId", "معرف العقار غير صالح");

            if (request.PageNumber <= 0 || request.PageSize <= 0)
                throw new BusinessRuleException("InvalidPagination", "رقم الصفحة وحجم الصفحة يجب أن يكونا أكبر من صفر");

            var role = _currentUserService.Role;
            if (role != "Admin" && _currentUserService.PropertyId != request.PropertyId)
            {
                _logger.LogWarning("ليس لدى المستخدم صلاحيات لعرض بلاغات هذا العقار");
                throw new ForbiddenException("ليس لديك صلاحية الوصول إلى بلاغات هذا العقار");
            }

            var allReports = await _reportRepository.GetReportsAsync(null, null, request.PropertyId, cancellationToken);
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