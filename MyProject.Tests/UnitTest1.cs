namespace MyProject.Tests;

using Microsoft.EntityFrameworkCore;
using TaskManagementAPI;
using TaskManagementAPI.Db;
using TaskManagementAPI.Models;
using Xunit;

public class UnitTest1:IDisposable
{
    private readonly ApplicationDbContext _context;
    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer("Server=DESKTOP-5U547DJ; Database=TEST_TaskManagement; Trusted_Connection=true; Trust Server Certificate=true; MultipleActiveResultSets=true; Integrated Security=true;")
            .Options;

        _context = new ApplicationDbContext(options);
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
        //_context.Database.Migrate();


        
    }

    [Theory]
    [InlineData("John", "Doe", "john.doe@example.com", "username123","ValidPass123!",true)] // Ispravan unos
    [InlineData("John123", "Doe", "john.doe@example.com", "username123", "ValidPass123!",false)] // Ime sadrži brojeve
    [InlineData("John", "Doe123", "john.doe@example.com", "username123", "ValidPass123!", false)] //prezime sadrzi brojeve
    [InlineData("Alice", "Smith", "alice.smith@example", "username123", "ValidPass123!", false)] // Nevalidan email
    [InlineData("Bob", "Jones", "bob.jones@example.com", "username123", "pass", false)] // Prekratka lozinka
    [InlineData("John", "Doe", "john.doe@example.com", "username123", "password", false)] // Previše jednostavna lozinka
    [InlineData("Hacker", "Evil", "hacker@evil.com", "username123", "<script>alert('XSS');</script>", false)] // XSS napad
    [InlineData("John", "Doe", "john.doe@example.com", "username123", "DROP TABLE Users;--", false)] // SQL Injection
    //[Inli]
    public void Test1(string firstName,string lastName, string email, string username, string password,bool isValid)
    {
        //Arrange-priprema
        var user = new User { FirstName = firstName, LastName = lastName, Email = email,Username=username, Password = password };

        _context.Users.Add(user);
        _context.SaveChanges();
        var savedUser = _context.Users.FirstOrDefault(u => u.Email == email);
        if (isValid)
        {
            Assert.NotNull(savedUser);
            Assert.Equal(firstName, savedUser.FirstName);
            Assert.Equal(lastName, savedUser.LastName); 
            Assert.Equal(email, savedUser.Email);
            Assert.Equal(username, savedUser.Username);
            Assert.Equal(password, savedUser.Password);
        }
        else
        {
            Assert.Null(savedUser);
        }

        //var savedUser = _context.Users.FirstOrDefault(u=>u.Email==email);
        //Console.WriteLine("Saved user is:" + savedUser);

        //Assert.NotNull(savedUser);


        //Assert.Equal(firstName, savedUser.FirstName); 
        //Assert.Equal(lastName, savedUser.LastName); 
        //Assert.Equal(email, savedUser.Email);
        //Assert.Equal(username, savedUser.Username);
        //Assert.Equal(password, savedUser.Password);
        //Act-akcija

        //Assert-provera


        //Assert.True(true);
    }
    
    public void Dispose()
    {
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChanges();
    }
}