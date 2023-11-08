using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Vehicles.GetVehiclesByUserId
{
    public class GetVehiclesByUserIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
