namespace VTVApp.Api.Models.DTOs.Cities
{
    public class DeleteCityDto
    {
        public Guid Id { get; set; }
        public string ConfirmationMessage { get; set; } // Message confirming deletion or indicating issues
    }
}
