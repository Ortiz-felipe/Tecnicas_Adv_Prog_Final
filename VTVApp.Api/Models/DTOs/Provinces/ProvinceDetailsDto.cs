using VTVApp.Api.Models.DTOs.Cities;

namespace VTVApp.Api.Models.DTOs.Provinces
{
    public class ProvinceDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CityDetailsDto> Cities { get; set; }
        // Other relevant details about the province
    }
}
