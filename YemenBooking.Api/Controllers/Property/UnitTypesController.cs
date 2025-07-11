using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YemenBooking.Application.Commands.PropertyTypes;
using YemenBooking.Application.Commands.Units;
using YemenBooking.Application.Queries.PropertyTypes;
using YemenBooking.Application.Queries.Units;

namespace YemenBooking.Api.Controllers.Property
{
    /// <summary>
    /// متحكم بأنواع الوحدات لأصحاب العقارات
    /// Controller for unit type management by property owners
    /// </summary>
    public class UnitTypesController : BasePropertyController
    {
        public UnitTypesController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateUnitType([FromBody] CreateUnitTypeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{unitTypeId}")]
        public async Task<IActionResult> UpdateUnitType(Guid unitTypeId, [FromBody] UpdateUnitTypeCommand command)
        {
            command.UnitTypeId = unitTypeId;
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{unitTypeId}")]
        public async Task<IActionResult> DeleteUnitType(Guid unitTypeId)
        {
            var command = new DeleteUnitTypeCommand { UnitTypeId = unitTypeId };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{unitTypeId}")]
        public async Task<IActionResult> GetUnitTypeById(Guid unitTypeId)
        {
            var query = new GetUnitTypeByIdQuery { UnitTypeId = unitTypeId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("property-type/{propertyTypeId}")]
        public async Task<IActionResult> GetUnitTypesByPropertyType(Guid propertyTypeId)
        {
            var query = new GetUnitTypesByPropertyTypeQuery { PropertyTypeId = propertyTypeId };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
} 