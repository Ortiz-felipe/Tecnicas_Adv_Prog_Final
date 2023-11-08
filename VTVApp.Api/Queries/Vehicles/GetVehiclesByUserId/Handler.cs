using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Vehicles;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Vehicles.GetVehiclesByUserId
{
    public class Handler : IRequestHandler<GetVehiclesByUserIdQuery, IActionResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IVehicleRepository vehicleRepository, ILogger<Handler> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetVehiclesByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicles = await _vehicleRepository.GetVehiclesByUserIdAsync(request.UserId, cancellationToken);

                return vehicles == null ? this.NotFound(VehicleErrors.GetVehiclesForNonExistingUserError) : this.Ok(vehicles);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(VehicleErrors.GetVehiclesByUserIdError);
            }
        }
    }
}
