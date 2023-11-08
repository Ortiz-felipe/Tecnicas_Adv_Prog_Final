using VTVApp.Api.Models.DTOs.Users;

namespace VTVApp.Api.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(UserDto userInfo);
    }
}
