using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.Reports;
using YemenBooking.Application.Queries.Reports;

namespace YemenBooking.Api.Controllers.Admin
{
    /// <summary>
    /// متحكم بالتقارير للمدراء
    /// Controller for report management by admins
    /// </summary>
    public class ReportsController : BaseAdminController
    {
        public ReportsController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// إنشاء تقرير جديد
        /// Create a new report
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// تحديث تقرير
        /// Update an existing report
        /// </summary>
        [HttpPut("{reportId}")]
        public async Task<IActionResult> UpdateReport(Guid reportId, [FromBody] UpdateReportCommand command)
        {
            command.Id = reportId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// حذف تقرير
        /// Delete a report
        /// </summary>
        [HttpDelete("{reportId}")]
        public async Task<IActionResult> DeleteReport(Guid reportId)
        {
            var command = new DeleteReportCommand { Id = reportId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// جلب جميع التقارير
        /// Get all reports with pagination
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllReports([FromQuery] GetAllReportsQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب تقرير بواسطة المعرف
        /// Get a report by ID
        /// </summary>
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetReportById(Guid reportId)
        {
            var query = new GetReportByIdQuery { Id = reportId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب التقارير حسب عقار
        /// Get reports by property
        /// </summary>
        [HttpGet("property/{propertyId}")]
        public async Task<IActionResult> GetReportsByProperty(Guid propertyId, [FromQuery] GetReportsByPropertyQuery query)
        {
            query.PropertyId = propertyId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// جلب التقارير حسب مستخدم مبلّغ
        /// Get reports by reported user
        /// </summary>
        [HttpGet("reported-user/{reportedUserId}")]
        public async Task<IActionResult> GetReportsByReportedUser(Guid reportedUserId, [FromQuery] GetReportsByReportedUserQuery query)
        {
            query.UserId = reportedUserId;
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 