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
    /// معالج أمر تحديث تقييم
    /// </summary>
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, ResultDto<bool>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<UpdateReviewCommandHandler> _logger;

        public UpdateReviewCommandHandler(
            IReviewRepository reviewRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<UpdateReviewCommandHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء تحديث التقييم: ReviewId={ReviewId}", request.ReviewId);

            // التحقق من المدخلات
            if (request.ReviewId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف التقييم مطلوب");
            if (request.Cleanliness is null && request.Service is null && request.Location is null && request.Value is null && string.IsNullOrWhiteSpace(request.Comment))
                return ResultDto<bool>.Failed("يجب تحديد حقل واحد على الأقل للتحديث");

            // التحقق من الوجود
            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                return ResultDto<bool>.Failed("التقييم غير موجود");

            // التحقق من الصلاحيات
            if (_currentUserService.Role != "Admin" && review.CreatedBy != _currentUserService.UserId)
                return ResultDto<bool>.Failed("غير مصرح لك بتحديث هذا التقييم");

            // التحقق من فترة التعديل (24 ساعة)
            if (review.CreatedAt.AddHours(24) < DateTime.UtcNow)
                return ResultDto<bool>.Failed("انتهت فترة التعديل المسموحة على التقييم");

            // تنفيذ التحديث
            if (request.Cleanliness.HasValue)
            {
                if (request.Cleanliness < 1 || request.Cleanliness > 5)
                    return ResultDto<bool>.Failed("تقييم النظافة يجب أن يكون بين 1 و 5");
                review.Cleanliness = request.Cleanliness.Value;
            }
            if (request.Service.HasValue)
            {
                if (request.Service < 1 || request.Service > 5)
                    return ResultDto<bool>.Failed("تقييم الخدمة يجب أن يكون بين 1 و 5");
                review.Service = request.Service.Value;
            }
            if (request.Location.HasValue)
            {
                if (request.Location < 1 || request.Location > 5)
                    return ResultDto<bool>.Failed("تقييم الموقع يجب أن يكون بين 1 و 5");
                review.Location = request.Location.Value;
            }
            if (request.Value.HasValue)
            {
                if (request.Value < 1 || request.Value > 5)
                    return ResultDto<bool>.Failed("تقييم القيمة يجب أن يكون بين 1 و 5");
                review.Value = request.Value.Value;
            }
            if (!string.IsNullOrWhiteSpace(request.Comment))
                review.Comment = request.Comment.Trim();

            review.UpdatedBy = _currentUserService.UserId;
            review.UpdatedAt = DateTime.UtcNow;

            await _reviewRepository.UpdateReviewAsync(review, cancellationToken);

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "UpdateReview",
                $"تم تحديث التقييم {request.ReviewId}",
                request.ReviewId,
                "Review",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل تحديث التقييم بنجاح: ReviewId={ReviewId}", request.ReviewId);
            return ResultDto<bool>.Succeeded(true, "تم تحديث التقييم بنجاح");
        }
    }
} 