using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Users;

namespace VTVApp.Api.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<IActionResult>
    {
        [FromBody]
        public UserRegistrationDto Body { get; set; }
    }
}
