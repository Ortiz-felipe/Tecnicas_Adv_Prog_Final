namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserUpdateDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        // Other properties as needed for updates, like password, contact info, etc.
    }
}
