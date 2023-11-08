using VTVApp.Api.Services.Interfaces;

namespace VTVApp.Api.Services
{
    public class PasswordHashingService : IPasswordHashingService
    {
        public Task<string> HashPassword(string password)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.HashPassword(password));
        }

        public Task<bool> VerifyPassword(string password, string passwordHash)
        {
            return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, passwordHash));
        }
    }
}
