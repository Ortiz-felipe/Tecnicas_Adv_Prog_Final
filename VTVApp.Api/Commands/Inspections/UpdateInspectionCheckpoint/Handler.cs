using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Commands.Inspections.UpdateInspectionCheckpoint;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Inspections.InspectionErrors;

namespace VTVApp.Api.Commands.Inspections.UpdateInspection
{
    public class Handler : IRequestHandler<UpdateInspectionCheckpointCommand, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(UpdateInspectionCheckpointCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedInspection =
                    await _inspectionRepository.UpdateInspectionAsync(request.Body, cancellationToken);

                return !updatedInspection.Success ? this.NotFound(GetInspectionNotFoundError(request.InspectionId)) : this.Ok(updatedInspection.InspectionDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, UpdateInspectionError.Message);
                return this.InternalServerError(UpdateInspectionError);
            }
        }
    }
}
