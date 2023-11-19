namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class CreateVehicleDto
    {
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public Guid UserId { get; set; }
    }
}
