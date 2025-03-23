using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        RegistrationResultDTO RegisterUser(RegisterUserDTO registerUserDTO);
        LoginResponseDTO LoginUser(LoginUserDTO loginUserDTO);
        string RefreshToken(string refreshToken);
        List<UserDTO> GetUserDataForAdmin();
    }
}
