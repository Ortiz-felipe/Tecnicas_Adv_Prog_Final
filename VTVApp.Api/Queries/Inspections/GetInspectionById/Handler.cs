using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Inspections.InspectionErrors;

namespace VTVApp.Api.Queries.Inspections.GetInspectionById
{
    public class Handler : IRequestHandler<GetInspectionsByIdQuery, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetInspectionsByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inspection = await _inspectionRepository.GetInspectionByIdAsync(request.InspectionId, cancellationToken);

                return inspection == null ? this.NotFound(GetInspectionNotFoundError(request.InspectionId)) : this.Ok(inspection);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetInspectionDetailsError(request.InspectionId).Message);
                return this.InternalServerError(GetInspectionDetailsError(request.InspectionId));
            }
        }
    }
}
