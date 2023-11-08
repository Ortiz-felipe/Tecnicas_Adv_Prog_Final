namespace VTVApp.Api.Models.DTOs.Cities
{
    public class CreateCityDto
    {
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public int ProvinceId { get; set; } // Assuming the Province is identified by an integer ID
        // Include other properties necessary for creating a city
    }
}
