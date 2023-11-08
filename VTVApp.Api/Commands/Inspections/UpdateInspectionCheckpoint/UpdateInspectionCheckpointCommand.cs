using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Inspections;

namespace VTVApp.Api.Commands.Inspections.UpdateInspectionCheckpoint
{
    public class UpdateInspectionCheckpointCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid InspectionId { get; set; }
        [FromRoute]
        public Guid CheckpointId { get; set; }
        [FromBody]
        public UpdateInspectionDto Body { get; set; }
    }
}
