using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Inspections;

namespace VTVApp.Api.Commands.Inspections.StartInspection
{
    public class StartInspectionCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid AppointmentId { get; set; }

        [FromBody]
        public CreateInspectionDto Body { get; set; }
    }
}
