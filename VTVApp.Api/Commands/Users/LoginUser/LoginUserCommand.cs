using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Users;

namespace VTVApp.Api.Commands.Users.LoginUser
{
    public class LoginUserCommand : IRequest<IActionResult>
    {
        [FromBody]
        public UserAuthenticationDto Body { get; set; }
    }
}
