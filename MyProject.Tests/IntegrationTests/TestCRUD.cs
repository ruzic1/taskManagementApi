using Microsoft.EntityFrameworkCore;

//using NUnit.Framework;
using TaskManagementAPI.Db;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.MyProject.Tests.IntegrationTests
{
    public class TestCRUD : IDisposable
    {
        private readonly ApplicationDbContext _context;


        public TestCRUD()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=DESKTOP-5U547DJ; Database=TEST_TaskManagement; Trusted_Connection=true; Trust Server Certificate=true; MultipleActiveResultSets=true; Integrated Security=true;")
                .Options;

            _context = new ApplicationDbContext(options);

            //_context.Database.EnsureDeleted();
            //_context.Database.EnsureCreated();
        }

        

        [Fact]
        public void Test_CreateUser()
        {
            var user = new User { FirstName = "John", LastName = "Doe", Email = "john.doe@gmail.com", Username = "johndoe123", Password = "Johndoe123!" };
            _context.Users.Add(user);
            _context.SaveChanges();

            var savedUser = _context.Users.FirstOrDefault(x => x.Email == "john.doe@gmail.com");
            Assert.NotNull(savedUser);
            Assert.Equal("John", savedUser.FirstName);
        }
        [Fact]
        public void Test_DeleteUser()
        {
            var user = new User { FirstName = "Bob", Email = "bob@example.com" };
            _context.Users.Add(user);
            _context.SaveChanges();

            _context.Users.Remove(user);
            _context.SaveChanges();

            var deletedUser = _context.Users.FirstOrDefault(u => u.Email == "bob@example.com");
            Assert.Null(deletedUser);
        }
        public void Dispose()
        {
            _context.Dispose();
        }
        //[SetUp]
        //public void Setup()
        //{
        //    _context.Database.EnsureDeleted();
        //    _context.Database.EnsureCreated();
        //}
    }
}
