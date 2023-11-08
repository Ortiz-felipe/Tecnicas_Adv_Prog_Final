using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Checkpoints.UpdateCheckpointGrade
{
    public class UpdateCheckpointGradeCommand : IRequest<IActionResult>
    {
        public Guid CheckpointId { get; set; }
    }
}
