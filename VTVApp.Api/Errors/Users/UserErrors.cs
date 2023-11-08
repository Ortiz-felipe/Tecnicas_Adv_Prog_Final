namespace VTVApp.Api.Errors.Users
{
    public static class UserErrors
    {
        // Error when retrieving all users fails
        public static ApiError GetAllUsersError =>
            new(MajorErrorCodes.Users, 1, "Error occurred while retrieving all users.");

        // Error when a specific user cannot be found
        public static ApiError GetUserNotFoundError(Guid userId) =>
            new(MajorErrorCodes.Users, 2, $"User with ID {userId} could not be found.");

        // Error when creating a new user fails
        public static ApiError CreateUserError =>
            new(MajorErrorCodes.Users, 3, "Error occurred while creating a new user.");

        // Error when updating a user fails
        public static ApiError UpdateUserError =>
            new(MajorErrorCodes.Users, 4, "Error occurred while updating the user.");

        // Error when deleting a user fails
        public static ApiError DeleteUserError =>
            new(MajorErrorCodes.Users, 5, "Error occurred while attempting to delete the user.");

        // Error when user authentication fails
        public static ApiError UserAuthenticationFailedError =>
            new(MajorErrorCodes.Users, 6, "Authentication failed for the user.");

        // Error when user registration fails
        public static ApiError UserRegistrationFailedError =>
            new(MajorErrorCodes.Users, 7, "User registration failed due to an error.");

        // Error when there is a problem with the user's session or token
        public static ApiError UserSessionError =>
            new(MajorErrorCodes.Users, 8, "There was a problem with the user's session or token.");

        // Error when a user's operation is not authorized
        public static ApiError UserNotAuthorizedError =>
            new(MajorErrorCodes.Users, 9, "The user is not authorized to perform this operation.");

        // Error when a user email is already in use
        public static ApiError UserEmailInUseError =>
            new(MajorErrorCodes.Users, 10, "The user's email is already in use.");

        // Error when user is unable to login
        public static ApiError UserLoginError =>
            new(MajorErrorCodes.Users, 11, "The user is unable to login.");

        // Error when failing to get user details by Id.
        public static ApiError GetUserDetailsError =>
            new(MajorErrorCodes.Users, 12, "Error occurred while retrieving user details.");

        // Error for any unexpected issue during user operations
        public static ApiError UserUnexpectedError =>
            new(MajorErrorCodes.Users, 99, "An unexpected error occurred in user operations.");
    }

}
