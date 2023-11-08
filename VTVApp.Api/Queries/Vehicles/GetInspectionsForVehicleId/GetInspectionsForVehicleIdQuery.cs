using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Vehicles.GetInspectionsForVehicle
{
    public class GetInspectionsForVehicleIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
