using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories{
    public interface IRoleRepository{
        Task<Role> GetDefaultRoleFromDatabase();
    }
}