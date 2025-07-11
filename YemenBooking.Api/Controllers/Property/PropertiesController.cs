using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.Properties;
using YemenBooking.Application.Queries.Properties;
using YemenBooking.Application.Queries.Reviews;

namespace YemenBooking.Api.Controllers.Property
{
    /// <summary>
    /// متحكم بإدارة العقارات لأصحاب العقارات
    /// Controller for property management by property owners
    /// </summary>
    public class PropertiesController : BasePropertyController
    {
        public PropertiesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// جلب بيانات عقار بواسطة المعرف
        /// Get property data by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPropertyById(Guid id)
        {
            var query = new GetPropertyByIdQuery { PropertyId = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// إنشاء عقار جديد
        /// Create a new property
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateProperty([FromBody] CreatePropertyCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تحديث بيانات عقار
        /// Update an existing property
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty(Guid id, [FromBody] UpdatePropertyCommand command)
        {
            command.PropertyId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف عقار
        /// Delete a property
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProperty(Guid id)
        {
            var command = new DeletePropertyCommand { PropertyId = id };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// جلب تفاصيل العقار مع الحقول الديناميكية
        /// Get property details including dynamic fields
        /// </summary>
        [HttpGet("{id}/details")]
        public async Task<IActionResult> GetPropertyDetails(Guid id, [FromQuery] bool includeDynamicFields = true)
        {
            var query = new GetPropertyDetailsQuery { PropertyId = id, IncludeDynamicFields = includeDynamicFields };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب بيانات العقار للتحرير
        /// Get property data for edit form
        /// </summary>
        [HttpGet("{id}/for-edit")]
        public async Task<IActionResult> GetPropertyForEdit(Guid id, [FromQuery] Guid ownerId)
        {
            var query = new GetPropertyForEditQuery { PropertyId = id, OwnerId = ownerId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب حقول النموذج لنوع العقار
        /// Get form fields grouped by groups for a property type
        /// </summary>
        [HttpGet("form-fields/{propertyTypeId}")]
        public async Task<IActionResult> GetPropertyFormFields(Guid propertyTypeId, [FromQuery] GetPropertyFormFieldsQuery query)
        {
            query.PropertyTypeId = propertyTypeId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على العقارات حسب المدينة
        /// Get properties by city
        /// </summary>
        [HttpGet("by-city")]
        public async Task<IActionResult> GetPropertiesByCity([FromQuery] GetPropertiesByCityQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على عقارات المالك
        /// Get properties by owner
        /// </summary>
        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetPropertiesByOwner(Guid ownerId, [FromQuery] GetPropertiesByOwnerQuery query)
        {
            query.OwnerId = ownerId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على العقارات حسب النوع
        /// Get properties by type
        /// </summary>
        [HttpGet("type/{propertyTypeId}")]
        public async Task<IActionResult> GetPropertiesByType(Guid propertyTypeId, [FromQuery] GetPropertiesByTypeQuery query)
        {
            query.PropertyTypeId = propertyTypeId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// الحصول على إحصائيات تقييم العقار
        /// Get property rating statistics
        /// </summary>
        [HttpGet("{propertyId}/rating-stats")]
        public async Task<IActionResult> GetPropertyRatingStats(Guid propertyId, [FromQuery] GetPropertyRatingStatsQuery query)
        {
            query.PropertyId = propertyId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// استعلام للحصول على مرافق العقار
        /// Query to get property amenities
        /// </summary>
        [HttpGet("{propertyId}/amenities")]
        public async Task<IActionResult> GetPropertyAmenities(Guid propertyId, [FromQuery] GetPropertyAmenitiesQuery query)
        {
            query.PropertyId = propertyId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 