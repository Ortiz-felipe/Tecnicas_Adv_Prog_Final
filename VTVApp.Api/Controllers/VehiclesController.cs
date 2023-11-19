using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Commands.Vehicles.CreateVehicle;
using VTVApp.Api.Commands.Vehicles.DeleteVehicle;
using VTVApp.Api.Commands.Vehicles.UpdateVehicle;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Models.DTOs.Vehicle;
using VTVApp.Api.Queries.Vehicles.GetByVehicleId;
using VTVApp.Api.Queries.Vehicles.GetFavoriteVehicleByUserId;
using VTVApp.Api.Queries.Vehicles.GetInspectionsForVehicle;
using VTVApp.Api.Queries.Vehicles.GetVehiclesByUserId;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VehiclesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Register a new vehicle for a user
        [HttpPost(Name = "CreateVehicleAsync")]
        [ProducesResponseType(typeof(VehicleOperationResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVehicleAsync([FromBody] CreateVehicleCommand command)
        {
            return await _mediator.Send(command);
        }

        // Get a vehicle by its ID
        [HttpGet("{VehicleId}", Name = "GetVehicleByIdAsync")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehicleByIdAsync([FromRoute] GetByVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Update a vehicle's details
        [HttpPut("{VehicleId}", Name = "UpdateVehicleAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateVehicleAsync([FromBody] UpdateVehicleCommand command)
        {
            return await _mediator.Send(command);
        }

        // Delete a vehicle
        [HttpDelete("{VehicleId}", Name = "DeleteVehicleAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteVehicleAsync(DeleteVehicleCommand command)
        {
            return await _mediator.Send(command);
        }

        // Get all vehicles for a user
        [HttpGet("user/{UserId}", Name = "GetVehiclesByUserIdAsync")]
        [ProducesResponseType(typeof(IEnumerable<VehicleDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetVehiclesByUserIdAsync([FromRoute] GetVehiclesByUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get the inspection history for a vehicle
        [HttpGet("{VehicleId}/inspections", Name = "GetInspectionsForVehicleAsync")]
        [ProducesResponseType(typeof(IEnumerable<InspectionDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionsForVehicleAsync([FromRoute] GetInspectionsForVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get the vehicle marked as favorite for a user
        [HttpGet("user/{UserId}/favorite", Name = "GetFavoriteVehicleForUserAsync")]
        [ProducesResponseType(typeof(VehicleDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFavoriteVehicleForUserAsync(
            [FromRoute] GetFavoriteVehicleForUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }
    }
}
