using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Queries.UnitTypeFields;

namespace YemenBooking.Api.Controllers.Property
{
    /// <summary>
    /// متحكم بحقول نوع العقار لأصحاب العقارات
    /// Controller for property type fields viewing by property owners
    /// </summary>
    [ApiController]
    [Route("api/property/property-type-fields")]
    [Authorize(Roles = "PropertyOwner")]
    public class UnitTypeFieldsController : BasePropertyController
    {
        public UnitTypeFieldsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// جلب حقول نوع العقار
        /// Get property type fields for a given property type
        /// </summary>
        [HttpGet("property-type/{propertyTypeId}")]
        public async Task<IActionResult> GetUnitTypeFields(string propertyTypeId, [FromQuery] GetUnitTypeFieldsQuery query)
        {
            query.PropertyTypeId = propertyTypeId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب بيانات حقل نوع الوحدة بواسطة المعرف
        /// Get property type field by id
        /// </summary>
        [HttpGet("{fieldId}")]
        public async Task<IActionResult> GetUnitTypeFieldById(Guid fieldId, [FromQuery] GetUnitTypeFieldByIdQuery query)
        {
            query.FieldId = fieldId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب الحقول المجمعة لحقل نوع الوحدة
        /// Get grouped property type fields
        /// </summary>
        [HttpGet("grouped")]
        public async Task<IActionResult> GetUnitTypeFieldsGrouped([FromQuery] GetUnitTypeFieldsGroupedQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 