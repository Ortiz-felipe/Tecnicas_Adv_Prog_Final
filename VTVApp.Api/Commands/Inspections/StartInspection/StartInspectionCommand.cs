using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Inspections.StartInspection
{
    public class StartInspectionCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid InspectionId { get; set; }
    }
}
