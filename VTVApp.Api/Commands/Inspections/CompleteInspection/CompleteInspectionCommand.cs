using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Inspections.CompleteInspection
{
    public class CompleteInspectionCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid InspectionId { get; set; }
    }
}
