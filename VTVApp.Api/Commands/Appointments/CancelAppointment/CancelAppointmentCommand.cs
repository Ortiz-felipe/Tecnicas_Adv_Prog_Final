using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Appointments.CancelAppointment
{
    public class CancelAppointmentCommand : IRequest<IActionResult>
    {
    }
}
