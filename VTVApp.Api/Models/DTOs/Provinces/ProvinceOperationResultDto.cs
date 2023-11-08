namespace VTVApp.Api.Models.DTOs.Provinces
{
    public class ProvinceOperationResultDto
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } // Details about the operation result
    }
}
