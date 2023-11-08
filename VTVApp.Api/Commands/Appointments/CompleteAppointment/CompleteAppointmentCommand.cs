using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Appointments.CompleteAppointment
{
    public class CompleteAppointmentCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid AppointmentId { get; set; }
    }
}
