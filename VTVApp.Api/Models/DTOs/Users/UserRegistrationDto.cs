namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserRegistrationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // Should be encrypted/hash stored
        public Guid CityId { get; set; }
        public int ProvinceId { get; set; }
        public string PhoneNumber { get; set; }
        // Additional information like address might be necessary depending on the application's needs
    }
}
