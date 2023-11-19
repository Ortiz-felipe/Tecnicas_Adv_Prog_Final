namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleOperationResultDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
