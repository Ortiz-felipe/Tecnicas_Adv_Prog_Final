using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Commands.Inspections.CompleteInspection;
using VTVApp.Api.Commands.Inspections.StartInspection;
using VTVApp.Api.Commands.Inspections.UpdateInspection;
using VTVApp.Api.Commands.Inspections.UpdateInspectionCheckpoint;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Queries.Inspections.GetInspectionById;
using VTVApp.Api.Queries.Inspections.GetInspectionsByRecheckRequired;
using VTVApp.Api.Queries.Inspections.GetInspectionsByUserId;
using VTVApp.Api.Queries.Inspections.GetInspectionsByVehicleId;
using VTVApp.Api.Queries.Vehicles.GetLatestInspectionForVehicleId;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InspectionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InspectionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Start a new inspection
        [HttpPost("{AppointmentId}", Name = "StartInspectionAsync")]
        [ProducesResponseType(typeof(InspectionOperationResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartInspectionAsync([FromBody] StartInspectionCommand command)
        {
            return await _mediator.Send(command);
        }

        // Complete an inspection
        [HttpPut("{InspectionId}/complete", Name = "CompleteInspectionAsync")]
        [ProducesResponseType(typeof(InspectionOperationResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteInspectionAsync([FromBody] CompleteInspectionCommand command)
        {
            return await _mediator.Send(command);
        }

        // Get an inspection by ID
        [HttpGet("{InspectionId}", Name = "GetInspectionByIdAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionByIdAsync([FromRoute] GetInspectionsByIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get all inspections for a vehicle
        [HttpGet("vehicle/{VehicleId}", Name = "GetInspectionsByVehicleIdAsync")]
        [ProducesResponseType(typeof(IEnumerable<InspectionListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionsByVehicleIdAsync([FromRoute] GetInspectionsByVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get all inspections for a user
        [HttpGet("user/{UserId}", Name = "GetInspectionsByUserIdAsync")]
        [ProducesResponseType(typeof(IEnumerable<InspectionListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionsByUserIdAsync(
            [FromRoute] GetInspectionsByUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }


        // Update a specific checkpoint for an inspection
        [HttpPut("{InspectionId}/checkpoints/{CheckpointId}", Name = "UpdateInspectionCheckpointAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateInspectionCheckpointAsync([FromBody] UpdateInspectionCheckpointCommand command)
        {
            return await _mediator.Send(command);
        }

        // List inspections that require a recheck
        [HttpGet("rechecks", Name = "GetInspectionsRequiringRechecksAsync")]
        [ProducesResponseType(typeof(IEnumerable<InspectionListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionsRequiringRechecksAsync([FromRoute] GetInspectionsByRecheckRequiredQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get latest inspection for a vehicle
        [HttpGet("vehicle/{VehicleId}/latest", Name = "GetLatestInspectionForVehicleAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestInspectionForVehicleAsync(
            [FromRoute] GetLatestInspectionForVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }
    }
}
