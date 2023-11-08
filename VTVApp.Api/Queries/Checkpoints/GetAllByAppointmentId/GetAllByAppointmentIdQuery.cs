using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Checkpoints.GetAllByAppointmentId
{
    public class GetAllByAppointmentIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid AppointmentId { get; set; }
    }
}
