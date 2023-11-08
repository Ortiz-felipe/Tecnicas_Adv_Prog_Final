namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleDetailsDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string VIN { get; set; } // Vehicle Identification Number
        public string OwnerName { get; set; } // Optionally include owner details if relevant
        // Add any additional details relevant to vehicle specifications or requirements
    }
}
