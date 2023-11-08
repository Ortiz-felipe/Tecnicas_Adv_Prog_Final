using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Appointments.GetById
{
    public class GetByIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid AppointmentId { get; set; }
    }
}
