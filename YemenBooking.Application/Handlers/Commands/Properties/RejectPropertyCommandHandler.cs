using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using YemenBooking.Application.Commands.Properties;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Notifications;

namespace YemenBooking.Application.Handlers.Commands.Properties
{
    /// <summary>
    /// معالج أمر رفض العقار
    /// </summary>
    public class RejectPropertyCommandHandler : IRequestHandler<RejectPropertyCommand, ResultDto<bool>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;
        private readonly IAuditService _auditService;
        private readonly ILogger<RejectPropertyCommandHandler> _logger;

        public RejectPropertyCommandHandler(
            IPropertyRepository propertyRepository,
            ICurrentUserService currentUserService,
            INotificationService notificationService,
            IAuditService auditService,
            ILogger<RejectPropertyCommandHandler> logger)
        {
            _propertyRepository = propertyRepository;
            _currentUserService = currentUserService;
            _notificationService = notificationService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(RejectPropertyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء رفض العقار: PropertyId={PropertyId}", request.PropertyId);

            // التحقق من صحة المدخلات
            if (request.PropertyId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف العقار مطلوب");
            if (string.IsNullOrWhiteSpace(request.Reason))
                return ResultDto<bool>.Failed("سبب الرفض مطلوب");
            if (request.AdminId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف المسؤول مطلوب");

            // التحقق من الصلاحيات (مسؤول)
            if (_currentUserService.Role != "Admin")
                return ResultDto<bool>.Failed("غير مصرح لك برفض العقار");

            // التحقق من وجود العقار وحالته
            var property = await _propertyRepository.GetPropertyByIdAsync(request.PropertyId, cancellationToken);
            if (property == null)
                return ResultDto<bool>.Failed("العقار غير موجود");
            if (property.IsApproved)
                return ResultDto<bool>.Failed("العقار معتمد مسبقاً ولا يمكن رفضه");

            // تنفيذ الرفض
            var success = await _propertyRepository.RejectPropertyAsync(request.PropertyId, request.Reason, cancellationToken);
            if (!success)
                return ResultDto<bool>.Failed("فشل رفض العقار");

            // تسجيل العملية في سجل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "RejectProperty",
                $"تم رفض العقار {request.PropertyId} لسبب: {request.Reason}",
                request.PropertyId,
                "Property",
                _currentUserService.UserId,
                new Dictionary<string, object> { { "Reason", request.Reason } },
                cancellationToken);

            // إرسال إشعار للمالك
            await _notificationService.SendAsync(new NotificationRequest
            {
                UserId = property.OwnerId,
                Type = NotificationType.BookingCancelled,
                Title = "تم رفض العقار",
                Message = $"عذراً، تم رفض عقارك '{property.Name}' بسبب: {request.Reason}"
            }, cancellationToken);

            _logger.LogInformation("اكتمل رفض العقار: PropertyId={PropertyId}", request.PropertyId);
            return ResultDto<bool>.Succeeded(true, "تم رفض العقار بنجاح");
        }
    }
} 