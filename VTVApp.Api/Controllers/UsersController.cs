using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Commands.Inspections.CompleteInspection;
using VTVApp.Api.Commands.Inspections.StartInspection;
using VTVApp.Api.Commands.Users.CreateUser;
using VTVApp.Api.Commands.Users.DeleteUser;
using VTVApp.Api.Commands.Users.LoginUser;
using VTVApp.Api.Commands.Users.UpdateUser;
using VTVApp.Api.Errors;
using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Queries.Users.GetAll;
using VTVApp.Api.Queries.Users.GetByUserId;

namespace VTVApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Register a new user
        [HttpPost(Name = "RegisterUserAsync")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        // Get a user by their ID
        [HttpGet("{UserId}", Name = "GetUserByIdAsync")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserByIdAsync([FromRoute] GetByUserIdQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Update a user's details
        [HttpPut("{UserId}", Name = "UpdateUserAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UpdateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        // Delete a user
        [HttpDelete("{UserId}", Name = "DeleteUserAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteUserAsync(DeleteUserCommand command)
        {
            return await _mediator.Send(command);
        }

        // List all users
        [HttpGet(Name = "GetAllUsersAsync")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllUsersAsync([FromQuery] GetAllQuery queryRequest)
        {
            return await _mediator.Send(queryRequest);
        }

        // Login user
        [HttpPost("login", Name = "LoginUserAsync")]
        [ProducesResponseType(typeof(UserAuthenticationResultDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserCommand command)
        {
            return await _mediator.Send(command);
        }

        //// Inspectors only: Start an inspection process
        //[HttpPost("start-inspection/{appointmentId}", Name = "StartInspectionByInspectorAsync")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> StartInspectionByInspectorAsync(Guid appointmentId, [FromBody] StartInspectionCommand command)
        //{
        //    return Ok();
        //}

        //// Inspectors only: Complete an inspection process
        //[HttpPost("complete-inspection/{inspectionId}", Name = "CompleteInspectionByInspectorAsync")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(typeof(ExtendedProblemDetails), StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> CompleteInspectionByInspectorAsync(Guid inspectionId, [FromBody] CompleteInspectionCommand command)
        //{
        //    return Ok();
        //}
    }
}
