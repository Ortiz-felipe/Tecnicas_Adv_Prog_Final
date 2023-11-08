using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Vehicles;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Vehicles.UpdateVehicle
{
    public class Handler : IRequestHandler<UpdateVehicleCommand, IActionResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IVehicleRepository vehicleRepository, ILogger<Handler> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedVehicle = await _vehicleRepository.UpdateVehicleAsync(request.Body, cancellationToken);

                return !updatedVehicle.Success ? this.NotFound(VehicleErrors.GetVehicleNotFoundError(request.VehicleId)) : this.Ok(updatedVehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, VehicleErrors.UpdateVehicleError.Message);
                return this.InternalServerError(VehicleErrors.UpdateVehicleError);
            }
        }
    }
}
