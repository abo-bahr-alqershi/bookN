using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.UnitTypeFields;
using YemenBooking.Application.Queries.UnitTypeFields;

namespace YemenBooking.Api.Controllers.Admin
{
    /// <summary>
    /// متحكم بحقول نوع العقار للمدراء
    /// Controller for property type fields management by admins
    /// </summary>
    [ApiController]
    [Route("api/admin/property-type-fields")]
    [Authorize(Roles = "Admin")]
    public class UnitTypeFieldsController : BaseAdminController
    {
        public UnitTypeFieldsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// إنشاء حقل نوع للعقار
        /// Create a new property type field
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateUnitTypeField([FromBody] CreateUnitTypeFieldCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تحديث بيانات حقل نوع الوحدة
        /// Update an existing property type field
        /// </summary>
        [HttpPut("{fieldId}")]
        public async Task<IActionResult> UpdateUnitTypeField(string fieldId, [FromBody] UpdateUnitTypeFieldCommand command)
        {
            command.FieldId = fieldId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف حقل نوع الوحدة
        /// Delete a property type field
        /// </summary>
        [HttpDelete("{fieldId}")]
        public async Task<IActionResult> DeleteUnitTypeField(string fieldId)
        {
            var command = new DeleteUnitTypeFieldCommand { FieldId = fieldId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تبديل حالة تفعيل حقل النوع
        /// Toggle the status of a property type field
        /// </summary>
        [HttpPatch("{fieldId}/toggle-status")]
        public async Task<IActionResult> ToggleUnitTypeFieldStatus(string fieldId, [FromBody] ToggleUnitTypeFieldStatusCommand command)
        {
            command.FieldId = fieldId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// إعادة ترتيب حقول نوع العقار
        /// Reorder property type fields
        /// </summary>
        [HttpPost("reorder")]
        public async Task<IActionResult> ReorderUnitTypeFields([FromBody] ReorderUnitTypeFieldsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

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