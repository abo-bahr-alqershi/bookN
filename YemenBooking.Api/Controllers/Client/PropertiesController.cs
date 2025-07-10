using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Queries.Properties;
using YemenBooking.Application.Queries.SearchFilters;

namespace YemenBooking.Api.Controllers.Client
{
    /// <summary>
    /// متحكم بعرض العقارات للعميل
    /// Controller for property listings for clients
    /// </summary>
    public class PropertiesController : BaseClientController
    {
        public PropertiesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// الحصول على العقارات المميزة
        /// Get featured properties
        /// </summary>
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedProperties([FromQuery] GetFeaturedPropertiesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على الوجهات الشائعة
        /// Get popular destinations
        /// </summary>
        [HttpGet("popular-destinations")]
        public async Task<IActionResult> GetPopularDestinations([FromQuery] GetPopularDestinationsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على العقارات المقترحة للمستخدم
        /// Get recommended properties for a user
        /// </summary>
        [HttpGet("recommended")]
        public async Task<IActionResult> GetRecommendedProperties([FromQuery] GetRecommendedPropertiesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على العقارات القريبة من الموقع
        /// Get properties near a specific location
        /// </summary>
        [HttpGet("nearby")]
        public async Task<IActionResult> GetPropertiesNearLocation([FromQuery] GetPropertiesNearLocationQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 