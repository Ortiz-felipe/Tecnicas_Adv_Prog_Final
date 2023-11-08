using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Commands.Vehicles.UpdateVehicle
{
    public class UpdateVehicleCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid VehicleId { get; set; }
        [FromBody]
        public VehicleUpdateDto Body { get; set; }
    }
}
