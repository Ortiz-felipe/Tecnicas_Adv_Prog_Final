using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Users.GetByUserId
{
    public class GetByUserIdQuery : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
