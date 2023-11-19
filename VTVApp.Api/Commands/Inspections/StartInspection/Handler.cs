using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Inspections;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Inspections.StartInspection
{
    public class Handler : IRequestHandler<StartInspectionCommand, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(StartInspectionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var inspection = await _inspectionRepository.AddInspectionAsync(request.Body, cancellationToken);
                if (inspection == null && !inspection.Success)
                {
                    return this.BadRequest(InspectionErrors.StartInspectionError);
                }

                return this.CreatedAtRoute("GetInspectionByIdAsync",
                    new { inspectionId = inspection.InspectionDetails.Id }, inspection);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, InspectionErrors.StartInspectionError.Message);
                return this.InternalServerError(InspectionErrors.StartInspectionError);
            }
        }
    }
}
