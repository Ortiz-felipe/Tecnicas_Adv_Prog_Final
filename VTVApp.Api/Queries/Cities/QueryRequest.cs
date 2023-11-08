using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Cities
{
    public class QueryRequest : IRequest<IActionResult>
    {
        [FromQuery]
        public int ProvinceId { get; set; }
    }
}
