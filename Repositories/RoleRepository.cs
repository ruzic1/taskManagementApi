using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Db;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class RoleRepository:IRoleRepository{

        private readonly ApplicationDbContext _dbContext;
        public RoleRepository(ApplicationDbContext dbContext)
        {
            _dbContext=dbContext;
        }
        public async Task<Role> GetDefaultRoleFromDatabase()
        {
            return await _dbContext.Roles.FirstOrDefaultAsync(x=>x.RoleName=="User");
        }
    }
}