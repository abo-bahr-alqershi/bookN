using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.Commands.Images;
using YemenBooking.Application.DTOs;
using YemenBooking.Core.Interfaces;
using YemenBooking.Core.Interfaces.Services;
using YemenBooking.Core.Enums;

namespace YemenBooking.Application.Handlers.Commands.Images
{
    /// <summary>
    /// معالج أمر رفع صورة مع بيانات إضافية
    /// </summary>
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, ResultDto<string>>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IImageProcessingService _imageProcessingService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IAuditService _auditService;
        private readonly ILogger<UploadImageCommandHandler> _logger;

        public UploadImageCommandHandler(
            IFileStorageService fileStorageService,
            IImageProcessingService imageProcessingService,
            ICurrentUserService currentUserService,
            IAuditService auditService,
            ILogger<UploadImageCommandHandler> logger)
        {
            _fileStorageService = fileStorageService;
            _imageProcessingService = imageProcessingService;
            _currentUserService = currentUserService;
            _auditService = auditService;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task<ResultDto<string>> Handle(UploadImageCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("بدء رفع الصورة: Name={Name}, Type={Type}", request.Name, request.ImageType);

            // التحقق من المصادقة
            if (_currentUserService.UserId == Guid.Empty)
                return ResultDto<string>.Failed("يجب تسجيل الدخول لرفع الصور");

            // التحقق من صحة المدخلات
            if (request.File == null || request.File.FileContent == null || request.File.FileContent.Length == 0)
                return ResultDto<string>.Failed("ملف الصورة مطلوب");
            if (string.IsNullOrWhiteSpace(request.Name))
                return ResultDto<string>.Failed("اسم الملف مطلوب");
            if (string.IsNullOrWhiteSpace(request.Extension))
                return ResultDto<string>.Failed("امتداد الملف مطلوب");

            try
            {
                // تحويل المحتوى إلى تيار
                var stream = new MemoryStream(request.File.FileContent);

                // التحقق من صلاحية الصورة
                stream.Seek(0, SeekOrigin.Begin);
                var validationOptions = new ImageValidationOptions
                {
                    MaxFileSizeBytes = 5 * 1024 * 1024 // 5 ميغابايت كحد أقصى
                };
                var validationResult = await _imageProcessingService.ValidateImageAsync(stream, validationOptions, cancellationToken);
                if (!validationResult.IsValid)
                    return ResultDto<string>.Failed(validationResult.ValidationErrors, "فشل التحقق من صحة الصورة");

                // تحسين الصورة إذا طُلب
                if (request.OptimizeImage)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var compressResult = await _imageProcessingService.CompressImageAsync(stream, request.Quality ?? 85, null, cancellationToken);
                    if (compressResult.IsSuccess && compressResult.ProcessedImageBytes != null)
                    {
                        stream.Dispose();
                        stream = new MemoryStream(compressResult.ProcessedImageBytes);
                    }
                    else if (!compressResult.IsSuccess)
                    {
                        _logger.LogWarning("فشل تحسين الصورة: {Error}", compressResult.ErrorMessage);
                    }
                }

                // إنشاء صورة مصغرة إذا طُلب
                if (request.GenerateThumbnail)
                {
                    stream.Seek(0, SeekOrigin.Begin);
                    var thumbResult = await _imageProcessingService.GenerateThumbnailAsync(stream, cancellationToken: cancellationToken);
                    if (thumbResult.IsSuccess && thumbResult.ProcessedImageBytes != null)
                    {
                        var thumbName = $"{request.Name}_thumb{request.Extension}";
                        await _fileStorageService.UploadFileAsync(thumbResult.ProcessedImageBytes, thumbName, request.File.ContentType, request.ImageType.ToString(), cancellationToken);
                    }
                    else if (!thumbResult.IsSuccess)
                    {
                        _logger.LogWarning("فشل إنشاء الصورة المصغرة: {Error}", thumbResult.ErrorMessage);
                    }
                }

                // رفع الملف الرئيسي
                stream.Seek(0, SeekOrigin.Begin);
                var fileName = request.Name + request.Extension;
                var uploadResult = await _fileStorageService.UploadFileAsync(
                    stream,
                    fileName,
                    request.File.ContentType,
                    request.ImageType.ToString(),
                    cancellationToken);

                if (!uploadResult.IsSuccess || uploadResult.FileUrl == null)
                    return ResultDto<string>.Failed("حدث خطأ أثناء رفع الصورة");

                // تسجيل عملية الرفع في السجل
                await _auditService.LogBusinessOperationAsync(
                    "UploadImage",
                    $"تم رفع الصورة {fileName} من قبل المستخدم {_currentUserService.UserId}",
                    null,
                    "Image",
                    _currentUserService.UserId,
                    new System.Collections.Generic.Dictionary<string, object>
                    {
                        { "Path", uploadResult.FilePath }
                    },
                    cancellationToken);

                _logger.LogInformation("اكتمل رفع الصورة بنجاح: Url={Url}", uploadResult.FileUrl);
                return ResultDto<string>.Succeeded(uploadResult.FileUrl, "تم رفع الصورة بنجاح");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطأ في رفع الصورة");
                return ResultDto<string>.Failed("حدث خطأ غير متوقع أثناء رفع الصورة");
            }
        }
    }
} 