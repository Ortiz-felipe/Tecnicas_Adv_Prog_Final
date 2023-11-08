using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Appointments;

namespace VTVApp.Api.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<IActionResult>
    {
        [FromBody]
        public CreateAppointmentDto Body { get; set; }
    }
}
