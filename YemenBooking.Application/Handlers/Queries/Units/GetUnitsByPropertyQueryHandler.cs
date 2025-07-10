using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Queries.Units;
using YemenBooking.Core.Interfaces.Repositories;
using YemenBooking.Core.Interfaces.Services;

namespace YemenBooking.Application.Handlers.Queries.Units
{
    /// <summary>
    /// معالج استعلام الحصول على وحدات عقار محدد
    /// Query handler for GetUnitsByPropertyQuery
    /// </summary>
    public class GetUnitsByPropertyQueryHandler : IRequestHandler<GetUnitsByPropertyQuery, PaginatedResult<UnitDto>>
    {
        private readonly IUnitRepository _unitRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<GetUnitsByPropertyQueryHandler> _logger;

        public GetUnitsByPropertyQueryHandler(
            IUnitRepository unitRepository,
            ICurrentUserService currentUserService,
            ILogger<GetUnitsByPropertyQueryHandler> logger)
        {
            _unitRepository = unitRepository;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        public async Task<PaginatedResult<UnitDto>> Handle(GetUnitsByPropertyQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام وحدات العقار: {PropertyId}", request.PropertyId);

            var query = _unitRepository.GetQueryable()
                .AsNoTracking()
                .Include(u => u.Property)
                .Include(u => u.UnitType)
                .Include(u => u.FieldValues)
                .Where(u => u.PropertyId == request.PropertyId);

            var currentUser = await _currentUserService.GetCurrentUserAsync(cancellationToken);
            var role = _currentUserService.Role;
            bool isOwner = currentUser != null && _currentUserService.PropertyId == request.PropertyId;
            if (role != "Admin" && !isOwner)
                query = query.Where(u => u.IsAvailable && u.Property.IsApproved);

            if (request.IsAvailable.HasValue)
                query = query.Where(u => u.IsAvailable == request.IsAvailable.Value);

            if (request.MinBasePrice.HasValue)
                query = query.Where(u => u.BasePrice.Amount >= request.MinBasePrice.Value);

            if (request.MaxBasePrice.HasValue)
                query = query.Where(u => u.BasePrice.Amount <= request.MaxBasePrice.Value);

            if (request.MinCapacity.HasValue)
                query = query.Where(u => u.MaxCapacity >= request.MinCapacity.Value);

            if (!string.IsNullOrWhiteSpace(request.NameContains))
                query = query.Where(u => u.Name.Contains(request.NameContains));

            var totalCount = await query.CountAsync(cancellationToken);

            var units = await query
                .OrderBy(u => u.Name)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var dtos = units.Select(u => new UnitDto
            {
                Id = u.Id,
                PropertyId = u.PropertyId,
                UnitTypeId = u.UnitTypeId,
                Name = u.Name,
                BasePrice = new MoneyDto { Amount = u.BasePrice.Amount, Currency = u.BasePrice.Currency },
                CustomFeatures = u.CustomFeatures,
                IsAvailable = u.IsAvailable,
                PropertyName = u.Property.Name,
                UnitTypeName = u.UnitType.Name,
                PricingMethod = u.PricingMethod,
                FieldValues = u.FieldValues.Select(fv => new UnitFieldValueDto
                {
                    FieldId = fv.UnitTypeFieldId,
                    FieldValue = fv.FieldValue
                }).ToList()
            }).ToList();

            return new PaginatedResult<UnitDto>
            {
                Items = dtos,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                TotalCount = totalCount
            };
        }
    }
} 