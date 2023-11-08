namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleSearchResultDto
    {
        public IEnumerable<VehicleDto> Vehicles { get; set; }
        public string Query { get; set; } // The search query that led to these results
    }
}
