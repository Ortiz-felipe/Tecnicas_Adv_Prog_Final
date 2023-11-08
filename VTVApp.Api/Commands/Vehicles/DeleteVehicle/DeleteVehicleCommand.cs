using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Vehicles.DeleteVehicle
{
    public class DeleteVehicleCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
    }
}
