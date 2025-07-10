using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.Images;

namespace YemenBooking.Api.Controllers.Property
{
    /// <summary>
    /// متحكم برفع الصور للمالكين
    /// Controller for uploading images by property owners
    /// </summary>
    [ApiController]
    [Route("api/property/upload-image")]
    [Authorize(Roles = "PropertyOwner")]
    public class UploadImageController : BasePropertyController
    {
        public UploadImageController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// رفع صورة مع بيانات إضافية
        /// Upload an image with additional data
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> UploadImage([FromBody] UploadImageCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
} 