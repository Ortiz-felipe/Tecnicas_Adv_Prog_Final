using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Appointments.GetLatestAppointmentByUserID
{
    public class GetLatestAppointmentByUserIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
