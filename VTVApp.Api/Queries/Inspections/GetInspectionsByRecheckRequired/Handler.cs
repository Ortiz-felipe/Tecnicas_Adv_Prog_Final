using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Inspections;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByRecheckRequired
{
    public class Handler : IRequestHandler<GetInspectionsByRecheckRequiredQuery, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetInspectionsByRecheckRequiredQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inspections = await _inspectionRepository.GetInspectionsByRecheckRequiredAsync(cancellationToken);

                return this.Ok(inspections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, InspectionErrors.GetRecheckRequiredInspectionsError.Message);
                return this.InternalServerError(InspectionErrors.GetRecheckRequiredInspectionsError);
            }
        }
    }
}
