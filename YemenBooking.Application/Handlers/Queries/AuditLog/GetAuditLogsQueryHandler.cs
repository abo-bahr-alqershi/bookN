using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Exceptions;
using YemenBooking.Application.Queries.AuditLog;
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Application.Handlers.Queries.AuditLog
{
    /// <summary>
    /// معالج استعلام الحصول على سجلات التدقيق مع فلترة حسب المستخدم أو الفترة الزمنية
    /// Handler for GetAuditLogsQuery
    /// </summary>
    public class GetAuditLogsQueryHandler : IRequestHandler<GetAuditLogsQuery, ResultDto<IEnumerable<AuditLogDto>>>
    {
        private readonly IAuditService _auditService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetAuditLogsQueryHandler> _logger;

        public GetAuditLogsQueryHandler(
            IAuditService auditService,
            ICurrentUserService currentUserService,
            ILogger<GetAuditLogsQueryHandler> logger)
        {
            _auditService = auditService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<ResultDto<IEnumerable<AuditLogDto>>> Handle(GetAuditLogsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Processing GetAuditLogsQuery. UserId: {UserId}, From: {From}, To: {To}, SearchTerm: {SearchTerm}, OperationType: {OperationType}",
                request.UserId, request.From, request.To, request.SearchTerm, request.OperationType);

            var currentUser = await _currentUserService.GetCurrentUserAsync(cancellationToken);
            if (currentUser == null)
                return ResultDto<IEnumerable<AuditLogDto>>.Failure("يجب تسجيل الدخول لعرض سجلات التدقيق");

            if (!await _currentUserService.IsInRoleAsync("Admin"))
                return ResultDto<IEnumerable<AuditLogDto>>.Failure("ليس لديك صلاحية لعرض سجلات التدقيق");

            // جلب سجلات التدقيق
            var logs = await _auditService.GetAuditTrailAsync(
                entityType: null,
                entityId: null,
                performedBy: request.UserId,
                fromDate: request.From,
                toDate: request.To,
                cancellationToken: cancellationToken);

            // التحويل إلى DTO
            var dtos = logs.Select(log => new AuditLogDto
            {
                Id = log.Id,
                TableName = log.EntityType,
                Action = log.Action.ToString(),
                RecordId = log.EntityId ?? Guid.Empty,
                UserId = log.PerformedBy ?? Guid.Empty,
                Changes = log.Notes ?? string.Empty,
                Timestamp = log.CreatedAt
            });

            return ResultDto<IEnumerable<AuditLogDto>>.Ok(dtos);
        }
    }
} 