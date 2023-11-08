using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Queries.Users.GetAll
{
    public class GetAllQuery : IRequest<IActionResult>
    {
    }
}
