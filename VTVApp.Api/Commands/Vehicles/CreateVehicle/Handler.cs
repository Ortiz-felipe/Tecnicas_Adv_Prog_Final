using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Vehicles.VehicleErrors;

namespace VTVApp.Api.Commands.Vehicles.CreateVehicle
{
    public class Handler : IRequestHandler<CreateVehicleCommand, IActionResult>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IVehicleRepository vehicleRepository, ILogger<Handler> logger)
        {
            _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var createdVehicle = await _vehicleRepository.AddVehicleAsync(request.Body, cancellationToken);
                return this.CreatedAtRoute("GetVehicleByIdAsync", new { vehicleId = createdVehicle.Id }, createdVehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CreateVehicleError.Message);
                return this.InternalServerError(CreateVehicleError);
            }
        }
    }
}
