using MediatR;
using Microsoft.AspNetCore.Mvc;
using VTVApp.Api.Errors.Users;
using VTVApp.Api.Extensions;
using VTVApp.Api.Repositories.Interfaces;

namespace VTVApp.Api.Queries.Users.GetAll
{
    public class Handler : IRequestHandler<GetAllQuery, IActionResult>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUserRepository userRepository, ILogger<Handler> logger)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IActionResult> Handle(GetAllQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await _userRepository.GetAllUsersAsync(cancellationToken);

                return this.Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, UserErrors.GetAllUsersError.Message);
                return this.InternalServerError(UserErrors.GetAllUsersError);
            }
        }
    }
}
