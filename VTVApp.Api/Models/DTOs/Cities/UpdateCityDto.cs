namespace VTVApp.Api.Models.DTOs.Cities
{
    public class UpdateCityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int PostalCode { get; set; }
        // Other updatable fields as necessary
    }
}
