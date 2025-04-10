using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagementAPI.Controllers.ControllerAttributes;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        private List<User> users = new List<User>
        {
            new User{ Id = 1,FirstName="John Doe",LastName="doe@gmail.com"},
            new User{ Id = 2,FirstName="Jane Smith",LastName="smith@gmail.com"},
        };

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user=users.Find(u=>u.Id==id);
            if (user==null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [NoAuthRequiredFilter]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO userDto)
        {
            //return Ok();
            var isRegistered=await _userService.RegisterUser(userDto);
            if (isRegistered.Success)
            {
                return Ok(new { Message = "Registration successful!" });
            }
            else
            {
                return BadRequest(new { Errors = isRegistered.Errors });
            }
            //return null;
            //if (!isRegistered)
            //{
            //    return Conflict(new { message = "Error 409:User already exists or registration failed" });
            //}
            //return Ok(new {message="Status 200:User successfully added!!!"});

        }

        //[Authorize(Roles ="Admin")]
        [NoAuthRequiredFilter]
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginUserDTO userDto)
        {
            var token = _userService.LoginUser(userDto);
            //var refreshToken = _userService.LoginUser();
            if (token==null)
            {
                return Unauthorized("Error 401: Invalid credentials");
            }
            return Ok(new
            {
                JwtToken=token.JwtToken,
                RefreshToken=token.RefreshToken,
                
            });
        }


        [Authorize]
        [HttpPost("Profile")]
        public IActionResult Profile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
            if (userId == null)
            {
                return Unauthorized(new { message = "Token nije validan ili je istekao." });
            }
            return Ok(new
            {
                UserId = userId,
                Email = userEmail
            });
        }

        [HttpPost("RefreshToken")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            var refresh = _userService.RefreshToken(refreshToken);
            if (refresh==null)
            {
                return Unauthorized("Invalid or expired refresh token");
            }
            return Ok(refresh);

        }
        


        [Authorize(Roles ="Admin")]
        [HttpGet("AdminPanel")]
        public IActionResult GetAdminData()
        {
            if (User.IsInRole("Admin"))
            {
                var users=_userService.GetUserDataForAdmin();
                if (users.Count == 0)
                {
                    return NotFound(new { message = "Nema korisnika!" });
                }
                return Ok(users);
                //return Ok("Dobrodosao admine!!");
            }
            else
            {
                return StatusCode(403, new { message = "Korisnik nije admin. Pristup je zabranjen" });
            }
            //User
            //return Ok("Samo admin moze videti ovaj sadrzaj");
        }
        

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user=await _userService.DeleteUser(id);
            return user?Ok("Successful user deleting"):BadRequest("User deleting failed");
        }
        
        
        
        //[Authorize(Roles="")]
        //[HttpGet("Admin-dashboard")]
        //public IActionResult AdminDashboard()
        //{

        //}
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
