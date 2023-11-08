namespace VTVApp.Api.Models.DTOs.Cities
{
    public class CityOperationResultDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } // Details about the operation result
    }
}
