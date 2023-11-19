namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleUpdateDto
    {
        public Guid Id { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public bool IsFavorite { get; set; }
    }
}
