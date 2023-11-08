namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleUpdateDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
    }
}
