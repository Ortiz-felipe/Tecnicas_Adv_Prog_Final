namespace VTVApp.Api.Services.Interfaces
{
    public interface IPasswordHashingService
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string passwordHash);
    }
}
