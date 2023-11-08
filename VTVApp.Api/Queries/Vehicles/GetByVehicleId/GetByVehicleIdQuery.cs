using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Vehicles.GetByVehicleId
{
    public class GetByVehicleIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
