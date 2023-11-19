using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Inspections;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Vehicles.GetLatestInspectionForVehicleId
{
    public class Handler : IRequestHandler<GetLatestInspectionForVehicleIdQuery, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ILogger<Handler> logger, IInspectionRepository inspectionRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _inspectionRepository = inspectionRepository;
        }

        public async Task<IActionResult> Handle(GetLatestInspectionForVehicleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inspection = await _inspectionRepository.GetLatestInspectionByVehicleIdAsync(request.VehicleId, cancellationToken);

                return inspection == null ? this.NotFound(InspectionErrors.GetVehicleInspectionsError(request.VehicleId)) : this.Ok(inspection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, InspectionErrors.GetVehicleInspectionsError(request.VehicleId).Message);
                return this.InternalServerError(InspectionErrors.GetVehicleInspectionsError(request.VehicleId));
            }
        }
    }
}
