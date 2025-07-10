using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.FieldTypes;
using YemenBooking.Application.Queries.FieldTypes;

namespace YemenBooking.Api.Controllers.Admin
{
    /// <summary>
    /// متحكم بأنواع الحقول للمدراء
    /// Controller for managing field types by admins
    /// </summary>
    public class FieldTypesController : BaseAdminController
    {
        public FieldTypesController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// إنشاء نوع حقل جديد
        /// Create a new field type
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateFieldType([FromBody] CreateFieldTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تحديث نوع حقل موجود
        /// Update an existing field type
        /// </summary>
        [HttpPut("{fieldTypeId}")]
        public async Task<IActionResult> UpdateFieldType(Guid fieldTypeId, [FromBody] UpdateFieldTypeCommand command)
        {
            command.FieldTypeId = fieldTypeId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف نوع حقل
        /// Delete a field type
        /// </summary>
        [HttpDelete("{fieldTypeId}")]
        public async Task<IActionResult> DeleteFieldType(Guid fieldTypeId)
        {
            var command = new DeleteFieldTypeCommand { FieldTypeId = fieldTypeId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تبديل حالة نوع الحقل (مفعل/غير مفعل)
        /// Toggle the status of a field type (enabled/disabled)
        /// </summary>
        [HttpPatch("{fieldTypeId}/toggle-status")]
        public async Task<IActionResult> ToggleFieldTypeStatus(Guid fieldTypeId, [FromBody] ToggleFieldTypeStatusCommand command)
        {
            command.FieldTypeId = fieldTypeId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// جلب جميع أنواع الحقول مع الصفحات
        /// Get all field types with pagination
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllFieldTypes([FromQuery] GetAllFieldTypesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب نوع حقل بناءً على المعرف
        /// Get a field type by its ID
        /// </summary>
        [HttpGet("{fieldTypeId}")]
        public async Task<IActionResult> GetFieldTypeById(Guid fieldTypeId, [FromQuery] GetFieldTypeByIdQuery query)
        {
            query.FieldTypeId = fieldTypeId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// البحث في أنواع الحقول
        /// Search field types
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> SearchFieldTypes([FromQuery] SearchFieldTypesQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 