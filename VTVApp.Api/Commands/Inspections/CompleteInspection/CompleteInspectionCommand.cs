using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Inspections;

namespace VTVApp.Api.Commands.Inspections.CompleteInspection
{
    public class CompleteInspectionCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid InspectionId { get; set; }

        public UpdateInspectionDto Body { get; set; }
    }
}
