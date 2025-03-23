using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public interface IUserRepository
    {
        bool Add(User user);
        User GetUserByEmailAndPassword(string email,string password);
        void UpdateUser(User user);
        User GetUserByRefreshToken(string refreshToken);
        List<User> GetUsersForAdmin();



        //void Delete(User user);
        //void DeleteById(int id);

    }
}
