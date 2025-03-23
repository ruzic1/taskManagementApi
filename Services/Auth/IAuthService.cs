using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Auth
{
    public interface IAuthService
    {
        public string GenerateJwtToken(User user);
        public string GenerateRefreshToken();
    }
}
