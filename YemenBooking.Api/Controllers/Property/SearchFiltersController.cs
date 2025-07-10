using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Queries.UnitTypeFields;
using YemenBooking.Application.Queries.SearchFilters;

namespace YemenBooking.Api.Controllers.Property
{
    /// <summary>
    /// متحكم بعرض فلاتر البحث لأصحاب العقارات
    /// Controller for property owners to view search filters
    /// </summary>
    [ApiController]
    [Route("api/property/search-filters")]
    [Authorize(Roles = "PropertyOwner")]
    public class SearchFiltersController : BasePropertyController
    {
        public SearchFiltersController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// جلب جميع فلاتر البحث لنوع عقار معين
        /// Get all search filters for a specific property type
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetSearchFilters([FromQuery] GetSearchFiltersQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب فلتر بحث حسب المعرف
        /// Get a search filter by its ID
        /// </summary>
        [HttpGet("{filterId}")]
        public async Task<IActionResult> GetSearchFilterById(Guid filterId)
        {
            var query = new GetSearchFilterByIdQuery { FilterId = filterId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب الحقول القابلة للبحث لنوع عقار معين
        /// Get searchable fields for a specific property type
        /// </summary>
        [HttpGet("searchable-fields")]
        public async Task<IActionResult> GetSearchableFields([FromQuery] GetSearchableFieldsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 