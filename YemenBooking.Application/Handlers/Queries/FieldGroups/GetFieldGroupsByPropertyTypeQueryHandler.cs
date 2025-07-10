using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Exceptions;
using YemenBooking.Application.Queries.FieldGroups;
using YemenBooking.Core.Interfaces.Repositories;

namespace YemenBooking.Application.Handlers.Queries.FieldGroups
{
    /// <summary>
    /// معالج استعلام جلب مجموعات الحقول لنوع عقار معين
    /// Query handler for GetFieldGroupsByPropertyTypeQuery
    /// </summary>
    public class GetFieldGroupsByPropertyTypeQueryHandler : IRequestHandler<GetFieldGroupsByPropertyTypeQuery, List<FieldGroupDto>>
    {
        private readonly IFieldGroupRepository _groupRepository;
        private readonly ILogger<GetFieldGroupsByPropertyTypeQueryHandler> _logger;

        public GetFieldGroupsByPropertyTypeQueryHandler(
            IFieldGroupRepository groupRepository,
            ILogger<GetFieldGroupsByPropertyTypeQueryHandler> logger)
        {
            _groupRepository = groupRepository;
            _logger = logger;
        }

        public async Task<List<FieldGroupDto>> Handle(GetFieldGroupsByPropertyTypeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("جاري معالجة استعلام جلب مجموعات الحقول لنوع العقار: {PropertyTypeId}", request.PropertyTypeId);

            if (!Guid.TryParse(request.PropertyTypeId, out var typeId))
                throw new ValidationException(nameof(request.PropertyTypeId), "معرف نوع العقار غير صالح");

            var groups = await _groupRepository.GetGroupsByPropertyTypeIdAsync(typeId, cancellationToken);
            var dtos = groups
                .OrderBy(g => g.SortOrder)
                .Select(g => new FieldGroupDto
                {
                    GroupId = g.Id.ToString(),
                    PropertyTypeId = g.UnitTypeId.ToString(),
                    GroupName = g.GroupName,
                    DisplayName = g.DisplayName,
                    Description = g.Description,
                    SortOrder = g.SortOrder,
                    IsCollapsible = g.IsCollapsible,
                    IsExpandedByDefault = g.IsExpandedByDefault
                })
                .ToList();

            return dtos;
        }
    }
} 