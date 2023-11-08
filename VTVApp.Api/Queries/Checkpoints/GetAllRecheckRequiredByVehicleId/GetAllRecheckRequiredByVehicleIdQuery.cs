using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Checkpoints.GetAllRecheckRequiredByVehicleId
{
    public class GetAllRecheckRequiredByVehicleIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
