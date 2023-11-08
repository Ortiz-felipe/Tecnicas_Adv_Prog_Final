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
using VTVApp.Api.Queries.Inspections.GetInspectionsByVehicleId;

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
        [HttpPost("{inspectionId}", Name = "StartInspectionAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> StartInspectionAsync([FromBody] StartInspectionCommand command)
        {
            return await _mediator.Send(command);
        }

        // Complete an inspection
        [HttpPut("{inspectionId}/complete", Name = "CompleteInspectionAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteInspectionAsync([FromBody] CompleteInspectionCommand command)
        {
            return await _mediator.Send(command);
        }

        // Get an inspection by ID
        [HttpGet("{inspectionId}", Name = "GetInspectionByIdAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionByIdAsync(GetInspectionsByIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Get all inspections for a vehicle
        [HttpGet("vehicle/{vehicleId}", Name = "GetInspectionsByVehicleIdAsync")]
        [ProducesResponseType(typeof(IEnumerable<InspectionListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionsByVehicleIdAsync(GetInspectionsByVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Update a specific checkpoint for an inspection
        [HttpPut("{inspectionId}/checkpoints/{checkpointId}", Name = "UpdateInspectionCheckpointAsync")]
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
        public async Task<IActionResult> GetInspectionsRequiringRechecksAsync(GetInspectionsByRecheckRequiredQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }
    }
}
