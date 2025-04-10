using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Add(User user);
        User GetUserByEmailAndPassword(string email,string password);
        void UpdateUser(User user);
        User GetUserByRefreshToken(string refreshToken);
        List<User> GetUsersForAdmin();

        Task<bool> RemoveUser(int id);

        //void Delete(User user);
        //void DeleteById(int id);

    }
}
