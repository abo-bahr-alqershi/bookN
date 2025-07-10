using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Reviews;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Interfaces;

namespace YemenBooking.Application.Handlers.Commands.Reviews
{
    /// <summary>
    /// معالج أمر حذف تقييم
    /// </summary>
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, ResultDto<bool>>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<DeleteReviewCommandHandler> _logger;

        public DeleteReviewCommandHandler(
            IReviewRepository reviewRepository,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<DeleteReviewCommandHandler> logger)
        {
            _reviewRepository = reviewRepository;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        public async Task<ResultDto<bool>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء حذف التقييم: ReviewId={ReviewId}", request.ReviewId);

            // التحقق من المدخلات
            if (request.ReviewId == Guid.Empty)
                return ResultDto<bool>.Failed("معرف التقييم مطلوب");

            // التحقق من الوجود
            var review = await _reviewRepository.GetReviewByIdAsync(request.ReviewId, cancellationToken);
            if (review == null)
                return ResultDto<bool>.Failed("التقييم غير موجود");

            // التحقق من الصلاحيات
            if (_currentUserService.Role != "Admin" && review.CreatedBy != _currentUserService.UserId)
                return ResultDto<bool>.Failed("غير مصرح لك بحذف هذا التقييم");

            // تنفيذ الحذف
            bool deleted = await _reviewRepository.DeleteReviewAsync(request.ReviewId, cancellationToken);
            if (!deleted)
                return ResultDto<bool>.Failed("فشل حذف التقييم");

            // تسجيل التدقيق
            await _auditService.LogBusinessOperationAsync(
                "DeleteReview",
                $"تم حذف التقييم {request.ReviewId}",
                request.ReviewId,
                "Review",
                _currentUserService.UserId,
                null,
                cancellationToken);

            _logger.LogInformation("اكتمل حذف التقييم بنجاح: ReviewId={ReviewId}", request.ReviewId);
            return ResultDto<bool>.Succeeded(true, "تم حذف التقييم بنجاح");
        }
    }
} 