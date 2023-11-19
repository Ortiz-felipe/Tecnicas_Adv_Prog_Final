namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProvinceName { get; set; }
        public string CityName { get; set; }
        public int UserRole { get; set; }
    }
}
