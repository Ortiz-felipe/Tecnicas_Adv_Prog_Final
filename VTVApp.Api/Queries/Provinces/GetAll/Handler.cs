using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Provinces.ProvincesErrors;

namespace VTVApp.Api.Queries.Provinces.GetAll
{
    public class Handler : IRequestHandler<QueryRequest, IActionResult>
    {
        private readonly IProvinceRepository _provincesRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IProvinceRepository provincesRepository, ILogger<Handler> logger)
        {
            _provincesRepository = provincesRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(QueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var provinces = await _provincesRepository.GetAllProvincesAsync(cancellationToken);

                return this.Ok(provinces);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetAllProvincesError.Message);
                return this.InternalServerError(GetAllProvincesError);
            }
        }
    }
}
