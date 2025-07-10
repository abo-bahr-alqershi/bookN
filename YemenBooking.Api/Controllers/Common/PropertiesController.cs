using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Queries.Properties;

namespace YemenBooking.Api.Controllers.Common
{
    /// <summary>
    /// متحكم بعملية البحث عن العقارات
    /// Controller for common property search operations
    /// </summary>
    [ApiController]
    [Route("api/common/[controller]")]
    public class PropertiesController : BaseCommonController
    {
        public PropertiesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// البحث في العقارات مع الفلاتر، الموقع، والمسافة
        /// Search properties with filters, location, and distance
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchProperties([FromQuery] SearchPropertiesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 