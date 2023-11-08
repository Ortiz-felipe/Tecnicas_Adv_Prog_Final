using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Checkpoints;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Checkpoints.GetAllRecheckRequiredByVehicleId
{
    public class Handler : IRequestHandler<GetAllRecheckRequiredByVehicleIdQuery, IActionResult>
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ICheckpointRepository checkpointRepository, ILogger<Handler> logger)
        {
            _checkpointRepository =
                checkpointRepository ?? throw new ArgumentNullException(nameof(checkpointRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetAllRecheckRequiredByVehicleIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkpoints = await _checkpointRepository.GetAllRecheckRequiredByVehicleIdAsync(request.VehicleId, cancellationToken);
                return checkpoints == null ? this.NoContent() : this.Ok(checkpoints);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CheckpointErrors.GetRecheckRequiredCheckpointsByVehicleError.Message);
                return this.InternalServerError(CheckpointErrors.GetRecheckRequiredCheckpointsByVehicleError);
            }
        }
    }
}
