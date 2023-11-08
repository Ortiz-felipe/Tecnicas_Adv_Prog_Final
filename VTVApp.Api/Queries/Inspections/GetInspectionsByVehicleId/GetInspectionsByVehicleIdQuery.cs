using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByVehicleId
{
    public class GetInspectionsByVehicleIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
