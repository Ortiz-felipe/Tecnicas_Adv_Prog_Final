using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;
using static VTVApp.Api.Errors.Cities.CitiesErrors;

namespace VTVApp.Api.Queries.Cities
{
    public class Handler : IRequestHandler<QueryRequest, IActionResult>
    {
        private readonly ICityRepository _citiesRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(ICityRepository citiesRepository, ILogger<Handler> logger)
        {
            _citiesRepository = citiesRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(QueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var cities = await _citiesRepository.GetCitiesByProvinceId(request.ProvinceId, cancellationToken);
                return this.Ok(cities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetAllCitiesError.Message);
                return this.InternalServerError(GetAllCitiesError);
            }
        }
    }
}
