using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.Amenities;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.Amenities
{
    /// <summary>
    /// معالج استعلام الحصول على جميع المرافق
    /// Query handler for GetAllAmenitiesQuery
    /// </summary>
    public class GetAllAmenitiesQueryHandler : IRequestHandler<GetAllAmenitiesQuery, PaginatedResult<AmenityDto>>
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly ILogger<GetAllAmenitiesQueryHandler> _logger;

        public GetAllAmenitiesQueryHandler(
            IAmenityRepository amenityRepository,
            ILogger<GetAllAmenitiesQueryHandler> logger)
        {
            _amenityRepository = amenityRepository;
            _logger = logger;
        }

        public async Task<PaginatedResult<AmenityDto>> Handle(GetAllAmenitiesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام جميع المرافق - الصفحة: {PageNumber}, الحجم: {PageSize}", request.PageNumber, request.PageSize);

            // التحقق من صحة معاملات الصفحة
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 10 : request.PageSize;

            // جلب جميع المرافق
            var amenities = (await _amenityRepository.GetAllAmenitiesAsync(cancellationToken)).ToList();

            // تطبيق بحث نصي اختياري
            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                var term = request.SearchTerm.Trim().ToLower();
                amenities = amenities
                    .Where(a => a.Name.ToLower().Contains(term) || a.Description.ToLower().Contains(term))
                    .ToList();
            }

            var totalCount = amenities.Count;

            // تطبيق الصفحات
            var items = amenities
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(a => new AmenityDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description
                })
                .ToList();

            _logger.LogInformation("تم جلب {Count} مرفق من إجمالي {TotalCount}", items.Count, totalCount);
            return new PaginatedResult<AmenityDto>(items, pageNumber, pageSize, totalCount);
        }
    }
} 