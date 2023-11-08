using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Users;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Users.CreateUser
{
    public class Handler : IRequestHandler<CreateUserCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUserRepository userRepository, ILogger<Handler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var createdUser = await _userRepository.AddUserAsync(request.Body, cancellationToken);

                if (!createdUser.Success)
                {
                    return this.Conflict(UserErrors.UserEmailInUseError);
                }

                return this.Ok(createdUser);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, UserErrors.UserRegistrationFailedError.Message);
                return this.InternalServerError(UserErrors.UserRegistrationFailedError);
            }
        }
    }
}
