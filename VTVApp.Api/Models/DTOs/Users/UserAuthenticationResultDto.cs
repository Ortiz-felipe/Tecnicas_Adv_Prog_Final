namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserAuthenticationResultDto
    {
        /// <summary>
        /// Indicates whether the authentication was successful.
        /// </summary>
        public bool IsAuthenticated { get; set; }

        /// <summary>
        /// The authentication token that can be used for subsequent requests if authentication is successful.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Contains user data if authentication is successful.
        /// </summary>
        public UserDto User { get; set; }

        /// <summary>
        /// Any error message or information about the authentication attempt.
        /// </summary>
        public string ErrorMessage { get; set; }
    }

}
