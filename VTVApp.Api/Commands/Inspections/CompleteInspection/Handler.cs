using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Inspections;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Inspections.CompleteInspection
{
    public class Handler : IRequestHandler<CompleteInspectionCommand, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(CompleteInspectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var updatedInspection = await _inspectionRepository.UpdateInspectionAsync(request.Body, cancellationToken);
                return !updatedInspection.Success ? this.BadRequest(InspectionErrors.GetInspectionNotFoundError(request.InspectionId)) : this.Ok(updatedInspection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, InspectionErrors.UpdateInspectionError.Message);
                return this.InternalServerError(InspectionErrors.UpdateInspectionError);
            }
        }
    }
}
