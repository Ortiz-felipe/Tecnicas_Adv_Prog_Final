using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Vehicles;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Vehicles.GetFavoriteVehicleByUserId
{
    public class Handler : IRequestHandler<GetFavoriteVehicleForUserIdQuery, IActionResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IVehicleRepository vehicleRepository, ILogger<Handler> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetFavoriteVehicleForUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetFavoriteVehicleByUserIdAsync(request.UserId, cancellationToken);
                return vehicle == null ? this.NoContent() : this.Ok(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, VehicleErrors.GetFavoriteVehicleByUserIdError.Message);
                return this.InternalServerError(VehicleErrors.GetFavoriteVehicleByUserIdError);
            }
        }
    }
}
