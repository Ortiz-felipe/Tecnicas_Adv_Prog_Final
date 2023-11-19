using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Globalization;
using VTVApp.Api.Commands.Appointments.CancelAppointment;
using VTVApp.Api.Commands.Appointments.CompleteAppointment;
using VTVApp.Api.Commands.Appointments.CreateAppointment;
using VTVApp.Api.Commands.Appointments.RescheduleAppointment;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs.Appointments;
using VTVApp.Api.Queries.Appointments.GetAll;
using VTVApp.Api.Queries.Appointments.GetAvailableSlots;
using VTVApp.Api.Queries.Appointments.GetById;
using VTVApp.Api.Queries.Appointments.GetByRecheckRequired;
using VTVApp.Api.Queries.Appointments.GetByUserId;
using VTVApp.Api.Queries.Appointments.GetLatestAppointmentByUserID;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Retrieves all appointments
        [HttpGet(Name = "GetAllAppointmentsAsync")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAppointmentsAsync([FromQuery] GetAllQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieves a specific appointment by ID
        [HttpGet("{AppointmentId}", Name = "GetAppointmentByIdAsync")]
        [ProducesResponseType(typeof(AppointmentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentByIdAsync([FromRoute] GetByIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieves the latest appointment for a given user
        [HttpGet("latest/{UserId}", Name = "GetLatestAppointmentByUserIdAsync")]
        [ProducesResponseType(typeof(AppointmentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetLatestAppointmentByUserIdAsync([FromRoute] GetLatestAppointmentByUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Creates a new appointment
        [HttpPost(Name = "CreateAppointmentAsync")]
        [ProducesResponseType(typeof(AppointmentOperationResultDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAppointmentAsync([FromBody] CreateAppointmentCommand command)
        {
            return await _mediator.Send(command);
        }

        // Retrieves available slots for a given day
        [HttpGet("availableSlots/{Date}", Name = "GetAvailableSlotsAsync")]
        [ProducesResponseType(typeof(AvailableAppointmentSlotsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
            Summary = "Retrieves available slots for a given day",
            Description = "Requires date in 'YYYY-MM-dd' format")]
        //[SwaggerParameter("date", Required = true, Type = "string", Format = "date")]
        public async Task<IActionResult> GetAvailableSlotsAsync([FromRoute] GetAvailableSlotsQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieves all appointments for a user
        [HttpGet("user/{UserId}", Name = "GetAppointmentsByUserAsync")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAppointmentsByUserAsync([FromRoute] GetByUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Retrieves all appointments that need a recheck
        [HttpGet("recheckRequired", Name = "GetRecheckRequiredAppointmentsAsync")]
        [ProducesResponseType(typeof(IEnumerable<AppointmentListDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetRecheckRequiredAppointmentsAsync([FromRoute] GetByRecheckRequiredQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Updates the appointment status to "Completed" after inspection
        [HttpPost("{AppointmentId}/complete", Name = "CompleteAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompleteAppointmentAsync([FromBody] CompleteAppointmentCommand command)
        {
            return await _mediator.Send(command);
        }

        // Cancels an existing appointment
        [HttpPost("{AppointmentId}/cancel", Name = "CancelAppointmentAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CancelAppointmentAsync(CancelAppointmentCommand command)
        {
            return await _mediator.Send(command);
        }

        // Reschedules an existing appointment
        [HttpPost("{AppointmentId}/reschedule", Name = "RescheduleAppointmentAsync")]
        [ProducesResponseType(typeof(AppointmentListDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RescheduleAppointmentAsync([FromBody] RescheduleAppointmentCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}