using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Appointments;

namespace VTVApp.Api.Commands.Appointments.RescheduleAppointment
{
    public class RescheduleAppointmentCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid AppointmentId { get; set; }

        [FromBody]
        public UpdateAppointmentDto Body { get; set; }
    }
}
