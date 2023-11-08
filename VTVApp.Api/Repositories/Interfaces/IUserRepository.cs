using VTVApp.Api.Models.DTOs;
using VTVApp.Api.Models.DTOs.Users;
using VTVApp.Api.Models.Entities;

namespace VTVApp.Api.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of UserDto objects.</returns>
        Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves details for a single user by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A UserDetailsDto object if found, otherwise null.</returns>
        Task<UserDetailsDto?> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new user to the repository.
        /// </summary>
        /// <param name="createUserDto">The DTO containing the details of the user to be added.</param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<UserOperationResultDto> AddUserAsync(UserRegistrationDto createUserDto, CancellationToken cancellationToken);

        /// <summary>
        /// Updates an existing user's information.
        /// </summary>
        /// <param name="userUpdateDto">The DTO containing the updated details of the user.</param>
        /// <param name="cancellationToken"></param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<UserOperationResultDto> UpdateUserAsync(UserUpdateDto userUpdateDto, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a user from the repository by their ID.
        /// </summary>
        /// <param name="userId">The ID of the user to be deleted.</param>
        /// <returns>An OperationResultDto indicating the result of the operation.</returns>
        Task<OperationResultDto> DeleteUserAsync(Guid userId, CancellationToken cancellationToken);

        /// <summary>
        /// Attempts to authenticate a user based on the provided login credentials.
        /// </summary>
        /// <param name="credentials">The login credentials for authentication.</param>
        /// <returns>A UserAuthenticationResultDto indicating the result of the authentication attempt.</returns>
        Task<UserAuthenticationResultDto> AuthenticateUserAsync(UserAuthenticationDto credentials, CancellationToken cancellationToken);
    }

}
