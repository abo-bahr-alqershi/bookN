using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.FieldGroups;
using YemenBooking.Application.Queries.FieldGroups;
using YemenBooking.Application.DTOs;
using YemenBooking.Application.Commands.UnitTypeFields;

namespace YemenBooking.Api.Controllers.Admin
{
    /// <summary>
    /// متحكم بإدارة مجموعات الحقول للمدراء
    /// Controller for managing field groups by admins
    /// </summary>
    public class FieldGroupsController : BaseAdminController
    {
        public FieldGroupsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// إنشاء مجموعة حقول جديدة
        /// Create a new field group
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateFieldGroup([FromBody] CreateFieldGroupCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تحديث مجموعة حقول
        /// Update an existing field group
        /// </summary>
        [HttpPut("{groupId}")]
        public async Task<IActionResult> UpdateFieldGroup(string groupId, [FromBody] UpdateFieldGroupCommand command)
        {
            command.GroupId = groupId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف مجموعة حقول
        /// Delete a field group
        /// </summary>
        [HttpDelete("{groupId}")]
        public async Task<IActionResult> DeleteFieldGroup(string groupId)
        {
            var command = new DeleteFieldGroupCommand { GroupId = groupId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// إعادة ترتيب مجموعات الحقول
        /// Reorder field groups within a property type
        /// </summary>
        [HttpPost("reorder")]
        public async Task<IActionResult> ReorderFieldGroups([FromBody] ReorderFieldGroupsCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// جلب مجموعة حقول حسب المعرف
        /// Get field group by ID
        /// </summary>
        [HttpGet("{groupId}")]
        public async Task<IActionResult> GetFieldGroupById(string groupId)
        {
            var query = new GetFieldGroupByIdQuery { GroupId = groupId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب مجموعات الحقول لنوع عقار معين
        /// Get field groups by property type
        /// </summary>
        [HttpGet("property-type/{propertyTypeId}")]
        public async Task<IActionResult> GetFieldGroupsByPropertyType(string propertyTypeId)
        {
            var query = new GetFieldGroupsByPropertyTypeQuery { PropertyTypeId = propertyTypeId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// تخصيص حقل لمجموعة حقول
        /// Assign a field to a field group
        /// </summary>
        [HttpPost("{groupId}/assign-field")]
        public async Task<IActionResult> AssignFieldToGroup(string groupId, [FromBody] AssignFieldToGroupCommand command)
        {
            command.GroupId = groupId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 