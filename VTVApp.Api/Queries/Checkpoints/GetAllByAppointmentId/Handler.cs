using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Checkpoints;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Checkpoints.GetAllByAppointmentId
{
    public class Handler : IRequestHandler<GetAllByAppointmentIdQuery, IActionResult>
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ICheckpointRepository checkpointRepository, ILogger<Handler> logger)
        {
            _checkpointRepository =
                checkpointRepository ?? throw new ArgumentNullException(nameof(checkpointRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetAllByAppointmentIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkpoints = await _checkpointRepository.GetAllCheckpointsSummaryByAppointmentIdAsync(request.AppointmentId, cancellationToken);
                return checkpoints == null ? this.NoContent() : this.Ok(checkpoints);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CheckpointErrors.GetCheckpointsByAppointmentError.Message);
                return this.InternalServerError(CheckpointErrors.GetCheckpointsByAppointmentError);
            }
        }
    }
}
