using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Inspections.GetInspectionById
{
    public class GetInspectionsByIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid InspectionId { get; set; }
    }
}
