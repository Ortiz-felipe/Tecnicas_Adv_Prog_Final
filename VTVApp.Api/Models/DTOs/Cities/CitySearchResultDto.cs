namespace VTVApp.Api.Models.DTOs.Cities
{
    public class CitySearchResultDto
    {
        public IEnumerable<CityDetailsDto> Cities { get; set; }
        public string Query { get; set; } // The search query that led to these results
    }
}
