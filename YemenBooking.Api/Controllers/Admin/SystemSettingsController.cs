using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Interfaces.Services;
using YemenBooking.Application.DTOs;

namespace YemenBooking.Api.Controllers.Admin
{
    [Route("api/admin/system-settings")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SystemSettingsController : ControllerBase
    {
        private readonly ISystemSettingsService _settingsService;

        public SystemSettingsController(ISystemSettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        /// <summary>
        /// جلب إعدادات النظام
        /// Get system settings
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<ResultDto<Dictionary<string, string>>>> GetSettingsAsync(CancellationToken cancellationToken)
        {
            var settings = await _settingsService.GetSettingsAsync(cancellationToken);
            return Ok(ResultDto<Dictionary<string, string>>.Succeeded(settings));
        }

        /// <summary>
        /// حفظ أو تحديث إعدادات النظام
        /// Save or update system settings
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<ResultDto<bool>>> SaveSettingsAsync([FromBody] Dictionary<string, string> settings, CancellationToken cancellationToken)
        {
            await _settingsService.SaveSettingsAsync(settings, cancellationToken);
            return Ok(ResultDto<bool>.Succeeded(true));
        }
    }
} 