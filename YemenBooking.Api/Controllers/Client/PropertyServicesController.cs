using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Queries.Services;

namespace YemenBooking.Api.Controllers.Client
{
    /// <summary>
    /// متحكم بعرض خدمات العقار للعميل
    /// Controller for client to get property services
    /// </summary>
    [ApiController]
    [Route("api/client/property-services")]
    [Authorize(Roles = "Client")]
    public class PropertyServicesController : BaseClientController
    {
        public PropertyServicesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// جلب خدمات عقار معين
        /// Get services for a specific property
        /// </summary>
        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetPropertyServices(Guid propertyId)
        {
            var query = new GetPropertyServicesQuery { PropertyId = propertyId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب خدمة العقار بحسب المعرف
        /// Get property service by id
        /// </summary>
        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetServiceById(Guid serviceId)
        {
            var query = new GetServiceByIdQuery { ServiceId = serviceId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب الخدمات حسب النوع
        /// Get services by type
        /// </summary>
        [HttpGet("type/{serviceType}")]
        public async Task<IActionResult> GetServicesByType(string serviceType)
        {
            var query = new GetServicesByTypeQuery { ServiceType = serviceType };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 