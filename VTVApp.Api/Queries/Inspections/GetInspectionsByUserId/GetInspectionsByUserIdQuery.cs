using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByUserId
{
    public class GetInspectionsByUserIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
