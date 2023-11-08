namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleFilterDto
    {
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int? Year { get; set; }
        public string VIN { get; set; }
        public Guid? OwnerId { get; set; }
        // Additional filter criteria can be added here.
    }

}
