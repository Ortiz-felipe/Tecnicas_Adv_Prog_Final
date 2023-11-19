using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Inspections;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Inspections.GetInspectionsByUserId
{
    public class Handler : IRequestHandler<GetInspectionsByUserIdQuery, IActionResult>
    {
        private readonly IInspectionRepository _inspectionRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IInspectionRepository inspectionRepository, ILogger<Handler> logger)
        {
            _inspectionRepository =
                inspectionRepository ?? throw new ArgumentNullException(nameof(inspectionRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetInspectionsByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var inspections = await _inspectionRepository.GetInspectionsByUserIdAsync(request.UserId, cancellationToken);

                return this.Ok(inspections);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting inspections by user ID");
                return this.InternalServerError(InspectionErrors.GetAllInspectionsError);
            }
        }
    }
}
