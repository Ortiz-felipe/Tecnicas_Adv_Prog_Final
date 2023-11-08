using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByRecheckRequired
{
    public class GetInspectionsByRecheckRequiredQuery : IRequest<IActionResult>
    {
    }
}
