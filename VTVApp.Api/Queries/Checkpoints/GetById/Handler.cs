using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Checkpoints;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Checkpoints.GetById
{
    public class Handler: IRequestHandler<GetByIdQuery, IActionResult>
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ICheckpointRepository checkpointRepository, ILogger<Handler> logger)
        {
            _checkpointRepository =
                checkpointRepository ?? throw new ArgumentNullException(nameof(checkpointRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var checkpoint = await _checkpointRepository.GetCheckpointDetailAsync(request.CheckpointId, cancellationToken);

                return checkpoint is null ? this.NotFound(CheckpointErrors.GetCheckpointNotFoundError(request.CheckpointId)) : this.Ok(checkpoint);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, CheckpointErrors.GetCheckpointError.Message);
                return this.InternalServerError(CheckpointErrors.GetCheckpointError);
            }
        }
    }
}
