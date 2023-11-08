namespace VTVApp.Api.Models.DTOs.Vehicle
{
    public class VehicleDeleteDto
    {
        public Guid Id { get; set; }
        public string ConfirmationMessage { get; set; } // Message confirming deletion or indicating issues
    }
}
