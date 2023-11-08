namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserDeleteDto
    {
        public Guid Id { get; set; }
        public string ConfirmationMessage { get; set; } // Message confirming deletion or indicating issues
    }
}
