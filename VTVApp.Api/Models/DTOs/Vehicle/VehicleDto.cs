namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public bool isFavorite { get; set; }
        public int VehicleStatus { get; set; }
        public Guid InspectionId { get; set; }
        // Additional vehicle details as needed
    }
}
