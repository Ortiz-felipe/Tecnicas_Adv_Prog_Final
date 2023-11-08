using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Users;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Commands.Users.LoginUser
{
    public class Handler : IRequestHandler<LoginUserCommand, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUserRepository userRepository, ILogger<Handler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _userRepository.AuthenticateUserAsync(request.Body, cancellationToken);

                return !result.IsAuthenticated ? this.BadRequest(UserErrors.UserAuthenticationFailedError) : this.Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, UserErrors.UserLoginError.Message);
                return this.InternalServerError(UserErrors.UserLoginError);
            }
        }
    }
}
