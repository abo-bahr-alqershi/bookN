using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Reviews;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Entities;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;

namespace YemenBooking.Application.Handlers.Commands.Reviews
{
    /// <summary>
    /// معالج أمر إنشاء تقييم جديد
    /// </summary>
    public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, ResultDto<Guid>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<CreateReviewCommandHandler> _logger;

        public CreateReviewCommandHandler(
            IReviewRepository reviewRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<CreateReviewCommandHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<Guid>> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء إنشاء تقييم: BookingId={BookingId}", request.BookingId);

            // التحقق من المدخلات
            if (request.BookingId == Guid.Empty)
                return ResultDto<Guid>.Failed("معرف الحجز مطلوب");
            if (request.Cleanliness < 1 || request.Cleanliness > 5)
                return ResultDto<Guid>.Failed("تقييم النظافة يجب أن يكون بين 1 و 5");
            if (request.Service < 1 || request.Service > 5)
                return ResultDto<Guid>.Failed("تقييم الخدمة يجب أن يكون بين 1 و 5");
            if (request.Location < 1 || request.Location > 5)
                return ResultDto<Guid>.Failed("تقييم الموقع يجب أن يكون بين 1 و 5");
            if (request.Value < 1 || request.Value > 5)
                return ResultDto<Guid>.Failed("تقييم القيمة يجب أن يكون بين 1 و 5");

            // التحقق من وجود الحجز وأهلية التقييم
            var booking = await _reviewRepository.GetBookingByIdAsync(request.BookingId, cancellationToken);
            if (booking == null)
                return ResultDto<Guid>.Failed("الحجز غير موجود");
            if (booking.UserId != _currentUserService.UserId)
                return ResultDto<Guid>.Failed("غير مصرح لك بإنشاء تقييم لهذا الحجز");
            bool eligible = await _reviewRepository.CheckReviewEligibilityAsync(request.BookingId, _currentUserService.UserId, cancellationToken);
            if (!eligible)
                return ResultDto<Guid>.Failed("غير مسموح بإضافة تقييم جديد لهذا الحجز");

            // إنشاء الكيان
            var review = new Review
            {
                BookingId = request.BookingId,
                Cleanliness = request.Cleanliness,
                Service = request.Service,
                Location = request.Location,
                Value = request.Value,
                Comment = request.Comment.Trim(),
                CreatedBy = _currentUserService.UserId,
                CreatedAt = DateTime.UtcNow
            };
            var created = await _reviewRepository.CreateReviewAsync(review, cancellationToken);

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "CreateReview",
                $"تم إنشاء تقييم جديد {created.Id}",
                created.Id,
                "Review",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل إنشاء التقييم بنجاح: ReviewId={ReviewId}", created.Id);
            return ResultDto<Guid>.Succeeded(created.Id, "تم إنشاء التقييم بنجاح");
        }
    }
} 