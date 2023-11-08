namespace VTVApp.Api.Models.DTOs.Users
{
    public class UserOperationResultDto
    {
        public Guid Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } // Details about the operation result
    }
}
