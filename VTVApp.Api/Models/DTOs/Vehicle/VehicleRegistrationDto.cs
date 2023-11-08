namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleRegistrationDto
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; }
        public Guid OwnerId { get; set; } // Assuming a vehicle is tied to a user
        // Include other properties as necessary for the registration process
    }
}
