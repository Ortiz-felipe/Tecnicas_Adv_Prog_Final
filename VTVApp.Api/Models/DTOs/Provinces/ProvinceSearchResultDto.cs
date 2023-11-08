namespace VTVApp.Api.Models.DTOs.Provinces
{
    public class ProvinceSearchResultDto
    {
        public IEnumerable<ProvinceDto> Provinces { get; set; }
        public string Query { get; set; } // The search query that led to these results
    }
}
