using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Checkpoints.GetInspectionResultsByVehicleId
{
    public class GetInspectionResultsByVehicleIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
