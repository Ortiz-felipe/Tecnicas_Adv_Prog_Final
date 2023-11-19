using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Commands.Checkpoints.UpdateCheckpointGrade;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Checkpoints;
using VTVApp.Api.Models.DTOs.Inspections;
using VTVApp.Api.Queries.Checkpoints.GetAllByAppointmentId;
using VTVApp.Api.Queries.Checkpoints.GetAllRecheckRequiredByVehicleId;
using VTVApp.Api.Queries.Checkpoints.GetById;
using VTVApp.Api.Queries.Checkpoints.GetInspectionResultsByVehicleId;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckpointController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CheckpointController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Retrieves all checkpoints for a given appointment
        [HttpGet("appointment/{AppointmentId}", Name = "GetCheckpointsByAppointmentAsync")]
        [ProducesResponseType(typeof(IEnumerable<CheckpointListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCheckpointsByAppointmentAsync([FromRoute] GetAllByAppointmentIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieves a specific checkpoint by ID
        [HttpGet("{CheckpointId}", Name = "GetCheckpointByIdAsync")]
        [ProducesResponseType(typeof(CheckpointDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCheckpointByIdAsync([FromRoute] GetByIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Updates a checkpoint with the grading
        [HttpPut("{checkpointId}", Name = "UpdateCheckpointGradeAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateCheckpointGradeAsync([FromBody] UpdateCheckpointGradeCommand command)
        {
            return await _mediator.Send(command);
        }

        // Retrieves checkpoints that need a recheck for a specific vehicle
        [HttpGet("vehicle/{VehicleId}/recheckRequired", Name = "GetRecheckRequiredCheckpointsByVehicleAsync")]
        [ProducesResponseType(typeof(IEnumerable<CheckpointListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecheckRequiredCheckpointsByVehicleAsync(
            [FromRoute] GetAllRecheckRequiredByVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieve the inspection results for a specific vehicle
        [HttpGet("vehicle/{VehicleId}/inspectionResults", Name = "GetInspectionResultsByVehicleAsync")]
        [ProducesResponseType(typeof(InspectionDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetInspectionResultsByVehicleAsync(
            [FromRoute] GetInspectionResultsByVehicleIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }
    }
}
