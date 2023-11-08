using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Vehicles.VehicleErrors;

namespace VTVApp.Api.Queries.Vehicles.GetByVehicleId
{
    public class Handler : IRequestHandler<GetByVehicleIdQuery, IActionResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IVehicleRepository vehicleRepository, ILogger<Handler> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _vehicleRepository.GetVehicleByIdAsync(request.VehicleId, cancellationToken);

                return vehicle == null ? this.NotFound(GetVehicleNotFoundError(request.VehicleId)) : this.Ok(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetVehicleByIdError.Message);
                return this.InternalServerError(GetVehicleByIdError);
            }
        }
    }
}
