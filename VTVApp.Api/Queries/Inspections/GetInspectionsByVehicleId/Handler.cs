using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Inspections.InspectionErrors;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByVehicleId
{
    public class Handler : IRequestHandler<GetInspectionsByVehicleIdQuery, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetInspectionsByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var vehicle = await _inspectionRepository.GetInspectionsByVehicleIdAsync(request.VehicleId, cancellationToken);

                return vehicle == null ? this.NotFound(GetInspectionNotFoundError(request.VehicleId)) : this.Ok(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetInspectionDetailsError(request.VehicleId).Message);
                return this.InternalServerError(GetInspectionDetailsError(request.VehicleId));
            }
        }
    }
}
