using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Models.DTOs.Users;

namespace VTVApp.Api.Commands.Users.UpdateUser
{
    public class UpdateUserCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
        [FromBody]
        public UserUpdateDto Body { get; set; }
    }
}
