using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Cities;
using VTVApp.Api.Queries.Cities;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CitiesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<CityDetailsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllCitiesAsync([FromQuery] QueryRequest queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }
    }
}
