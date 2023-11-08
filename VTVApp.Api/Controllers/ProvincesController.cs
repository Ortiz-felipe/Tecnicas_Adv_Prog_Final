using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Provinces;
using VTVApp.Api.Queries.Provinces.GetAll;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvincesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProvincesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IList<ProvinceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllProvincesAsync([FromQuery] QueryRequest queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

    }
}
