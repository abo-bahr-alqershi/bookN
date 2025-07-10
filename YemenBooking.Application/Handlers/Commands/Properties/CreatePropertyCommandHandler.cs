using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Properties;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Notifications;

namespace YemenBooking.Application.Handlers.Commands.Properties
{
    /// <summary>
    /// معالج أمر إنشاء عقار جديد
    /// </summary>
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, ResultDto<Guid>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly INotificationService _notificationService;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreatePropertyCommandHandler> _logger;

        public CreatePropertyCommandHandler(
            IPropertyRepository propertyRepository,
            ICurrentUserService currentUserService,
            INotificationService notificationService,
            IAuditService auditService,
            ILogger<CreatePropertyCommandHandler> logger)
        {
            _propertyRepository = propertyRepository;
            _currentUserService = currentUserService;
            _notificationService = notificationService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<Guid>> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء عقار جديد: Name={Name}, OwnerId={OwnerId}", request.Name, request.OwnerId);

            // التحقق من صحة المدخلات
            if (string.IsNullOrWhiteSpace(request.Name))
                return ResultDto<Guid>.Failed("اسم العقار مطلوب");
            if (string.IsNullOrWhiteSpace(request.Address))
                return ResultDto<Guid>.Failed("عنوان العقار مطلوب");
            if (request.OwnerId == Guid.Empty)
                return ResultDto<Guid>.Failed("معرف المالك مطلوب");
            if (request.PropertyTypeId == Guid.Empty)
                return ResultDto<Guid>.Failed("معرف نوع العقار مطلوب");
            if (string.IsNullOrWhiteSpace(request.City))
                return ResultDto<Guid>.Failed("اسم المدينة مطلوب");
            if (request.StarRating < 1 || request.StarRating > 5)
                return ResultDto<Guid>.Failed("تقييم النجوم يجب أن يكون بين 1 و 5");
            if (request.Latitude < -90 || request.Latitude > 90)
                return ResultDto<Guid>.Failed("خط العرض يجب أن يكون بين -90 و 90");
            if (request.Longitude < -180 || request.Longitude > 180)
                return ResultDto<Guid>.Failed("خط الطول يجب أن يكون بين -180 و 180");

            // التحقق من وجود المالك ونوع العقار
            var owner = await _propertyRepository.GetOwnerByIdAsync(request.OwnerId, cancellationToken);
            if (owner == null)
                return ResultDto<Guid>.Failed("المالك غير موجود");
            var propertyType = await _propertyRepository.GetPropertyTypeByIdAsync(request.PropertyTypeId, cancellationToken);
            if (propertyType == null)
                return ResultDto<Guid>.Failed("نوع العقار غير موجود");

            // التحقق من الصلاحيات (مالك العقار أو مسؤول)
            if (_currentUserService.Role != "Admin" && request.OwnerId != _currentUserService.UserId)
                return ResultDto<Guid>.Failed("غير مصرح لك بإنشاء عقار جديد");

            // إنشاء الكيان بحالة انتظار الموافقة
            var property = new Property
            {
                OwnerId = request.OwnerId,
                TypeId = request.PropertyTypeId,
                Name = request.Name,
                Address = request.Address,
                Description = request.Description,
                City = request.City,
                Latitude = (decimal)request.Latitude,
                Longitude = (decimal)request.Longitude,
                StarRating = request.StarRating,
                IsApproved = false,
                CreatedBy = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _propertyRepository.CreatePropertyAsync(property, cancellationToken);

            // تسجيل العملية في سجل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "CreateProperty",
                $"تم إنشاء عقار جديد {created.Id} باسم {created.Name}",
                created.Id,
                "Property",
                _currentUserService.UserId,
                null,
                cancellationToken);

            // إرسال إشعار للمراجعة إلى المالك
            await _notificationService.SendAsync(new NotificationRequest
            {
                UserId = request.OwnerId,
                Type = NotificationType.BookingCreated,
                Title = "تم إنشاء العقار وينتظر الموافقة",
                Message = $"تم إنشاء العقار '{created.Name}' ويحتاج إلى موافقة الإدارة"
            }, cancellationToken);

            _logger.LogInformation("اكتمل إنشاء العقار: PropertyId={PropertyId}", created.Id);
            return ResultDto<Guid>.Succeeded(created.Id, "تم إنشاء العقار بنجاح وينتظر الموافقة");
        }
    }
} 