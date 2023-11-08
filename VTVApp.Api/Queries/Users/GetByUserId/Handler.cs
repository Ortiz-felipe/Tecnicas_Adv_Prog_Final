using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Users;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Users.GetByUserId
{
    public class Handler : IRequestHandler<GetByUserIdQuery, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUserRepository userRepository, ILogger<Handler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetByUserIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetUserByIdAsync(request.UserId, cancellationToken);

                return user is null
                    ? this.NotFound(UserErrors.GetUserNotFoundError(request.UserId))
                    : this.Ok(user);
            }
            catch (Exception ex)
            {
                return this.InternalServerError(UserErrors.GetUserDetailsError);
            }
        }
    }
}
