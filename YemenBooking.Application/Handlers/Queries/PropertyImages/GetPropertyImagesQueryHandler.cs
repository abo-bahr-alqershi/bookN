using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.PropertyImages;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Application.Exceptions;

namespace YemenBooking.Application.Handlers.Queries.PropertyImages
{
    /// <summary>
    /// معالج استعلام الحصول على صور العقار
    /// Query handler for GetPropertyImagesQuery
    /// </summary>
    public class GetPropertyImagesQueryHandler : IRequestHandler<GetPropertyImagesQuery, ResultDto<IEnumerable<PropertyImageDto>>>
    {
        private readonly IPropertyImageRepository _repo;
        private readonly ILogger<GetPropertyImagesQueryHandler> _logger;

        public GetPropertyImagesQueryHandler(IPropertyImageRepository repo, ILogger<GetPropertyImagesQueryHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task<ResultDto<IEnumerable<PropertyImageDto>>> Handle(GetPropertyImagesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام صور العقار: {PropertyId}", request.PropertyId);

            if (request.PropertyId == Guid.Empty)
                throw new ValidationException(nameof(request.PropertyId), "معرف العقار غير صالح");

            var images = await _repo.GetImagesByPropertyAsync(request.PropertyId, cancellationToken);

            var dtos = images.Select(img => new PropertyImageDto
            {
                Id = img.Id,
                PropertyId = img.PropertyId,
                UnitId = img.UnitId,
                Name = img.Name,
                Url = img.Url,
                SizeBytes = img.SizeBytes,
                Type = img.Type,
                Category = img.Category,
                Caption = img.Caption,
                AltText = img.AltText,
                Tags = img.Tags,
                IsMain = img.IsMain,
                DisplayOrder = img.DisplayOrder,
                UploadedAt = img.UploadedAt,
                Status = img.Status
            }).ToList();

            return ResultDto<IEnumerable<PropertyImageDto>>.Ok(dtos, "تم جلب صور العقار بنجاح");
        }
    }
} 