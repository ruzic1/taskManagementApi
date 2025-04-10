using Microsoft.EntityFrameworkCore;
using TaskManagementAPI.Db;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Repositories
{
    public class UserRepository:IUserRepository
    {
        //Repository communicates with database
        //therefore it must include dbcontext

        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<bool> Add(User userObj)
        {
            try
            {
                await _dbContext.Users.AddAsync(userObj);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            //_dbContext.Users.Add(userObj);
            //_dbContext.SaveChanges();
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            var userInDatabase = _dbContext.Users.Include(y=>y.Role).FirstOrDefault(x => x.Email == email );
            if (userInDatabase==null)
            {
                return null;
            }
            bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(password, userInDatabase.Password);

            return isPasswordCorrect?userInDatabase:null;
        }

        public User GetUserByRefreshToken(string refreshToken)
        {
            var userWithRefreshToken = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == refreshToken&&x.RefreshTokenExpiryTime>DateTime.UtcNow);
            if (userWithRefreshToken == null) 
            {
                return null;
            }
            return userWithRefreshToken;
            
        }
        public void UpdateUser(User user)
        {
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
        }
        public List<User> GetUsersForAdmin()
        {
            var users=new List<User>();
            users=_dbContext.Users.Select(x => new User
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
                Role=x.Role
            }).ToList();
            return users;
        }
    
        public async Task<bool> RemoveUser(int id)
        {
            try
            {
                var userForRemoving=await _dbContext.Users.FirstOrDefaultAsync(x=>x.Id==id);
                if(userForRemoving==null)
                {
                    throw new Exception("User not found");
                }
                _dbContext.Users.Remove(userForRemoving);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                
                throw new Exception("Error message: User deleting aborted");
            }
        }
    }
}
