using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Vehicle;

namespace VTVApp.Api.Commands.Vehicles.CreateVehicle
{
    public class CreateVehicleCommand : IRequest<IActionResult>
    {
        public CreateVehicleDto Body { get; set; }
    }
}
