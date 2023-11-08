using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace VTVApp.Api.Commands.Users.DeleteUser
{
    public class DeleteUserCommand : IRequest<IActionResult>
    {
        [FromRoute]
        public Guid UserId { get; set; }
    }
}
