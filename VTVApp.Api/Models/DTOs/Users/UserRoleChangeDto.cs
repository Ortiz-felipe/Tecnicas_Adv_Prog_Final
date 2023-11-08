namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserRoleChangeDto
    {
        public Guid Id { get; set; }
        public string NewRole { get; set; } // New role to be assigned to the user
    }
}
