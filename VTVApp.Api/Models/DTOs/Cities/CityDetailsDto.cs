using VTVApp.Api.Models.DTOs.Provinces;

namespace VTVApp.Api.Models.DTOs.Cities
{
    public class CityDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }
        public ProvinceDto Province { get; set; } // Nested DTO if province details are needed
        // Additional details if there are any other relevant attributes of a city
    }
}
