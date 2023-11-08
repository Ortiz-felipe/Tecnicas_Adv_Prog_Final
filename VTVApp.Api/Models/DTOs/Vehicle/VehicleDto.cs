namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }

        // Additional vehicle details as needed
    }
}
