using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using Task = System.Threading.Tasks.Task;

namespace TaskManagementAPI.Services
{
    public interface IUserService
    {
        Task<RegistrationResultDTO> RegisterUser(RegisterUserDTO registerUserDTO);
        LoginResponseDTO LoginUser(LoginUserDTO loginUserDTO);
        string RefreshToken(string refreshToken);
        List<UserDTO> GetUserDataForAdmin();
        Task<bool> DeleteUser(int id);
    }
}
