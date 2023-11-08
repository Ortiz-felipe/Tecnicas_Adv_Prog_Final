using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Checkpoints.GetById
{
    public class GetByIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid CheckpointId { get; set; }
    }
}
