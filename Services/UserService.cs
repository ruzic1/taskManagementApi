using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services.Auth;
using TaskManagementAPI.Services.Validation;
using BCrypt;

namespace TaskManagementAPI.Services
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public UserService(IUserRepository userRepParam,IAuthService auth)
        {
            _userRepository = userRepParam;
            _authService = auth;
        }
        public RegistrationResultDTO RegisterUser(RegisterUserDTO userDto)
        {
            var errors = new List<string>();

            var validator = new RegisterUserValidator();

            var validationResult=validator.Validate(userDto);
            if(!validationResult.IsValid)
            {
                errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
                return RegistrationResultDTO.FailureResult(errors);
            }
            //return null;

            var passwordEncrypted = BCrypt.Net.BCrypt.HashPassword(userDto.Password);

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Username = userDto.Username,
                Password = passwordEncrypted,
                Role=UserRole.User,
            };

            var createdUser = _userRepository.Add(user);
            return RegistrationResultDTO.SuccessResult();
            //if (createdUser != null)
            //{
            //    return RegistrationResultDTO.
            //}
            //return RegistrationResultDTO.
            //var validator = new RegisterUserValidator();
            //var validationResult = validator.Validate(userDto);
            //if (!validationResult.IsValid)
            //{
                
            //    return validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            //}

            //var passwordEncrypted = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            
            //var user = new User
            //{
            //    FirstName = userDto.FirstName,
            //    LastName = userDto.LastName,
            //    Email = userDto.Email,
            //    Username = userDto.Username,
            //    Password = passwordEncrypted
            //};
            //var createdUser=_userRepository.Add(user);

            //if (createdUser)timo 
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            
        }
        public LoginResponseDTO LoginUser(LoginUserDTO userDto)
        {
            if (userDto!=null)
            {

                var loggedUser = _userRepository.GetUserByEmailAndPassword(userDto.Email,userDto.Password);
                //bool isCorrect=BCrypt.Net.BCrypt.Verify()
                var jwtToken = _authService.GenerateJwtToken(loggedUser);
                var refreshToken = _authService.GenerateRefreshToken();

                loggedUser.RefreshToken = refreshToken;
                loggedUser.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
                _userRepository.UpdateUser(loggedUser);
                //loggedUser.Ref
                return new LoginResponseDTO
                {
                    JwtToken= jwtToken,
                    RefreshToken=refreshToken
                };
            }
            else
            {
                return null;
            }
            //var loggedUser=_userRepository.GetUserByEmailAndUsername()
        }
        
        public string RefreshToken(string refreshToken)
        {
            var user = _userRepository.GetUserByRefreshToken(refreshToken);
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return null;
            }
            if(user==null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                return null;
            }
            var newJwtToken = _authService.GenerateJwtToken(user);
            return newJwtToken;
            //return user;
        }

        public List<UserDTO> GetUserDataForAdmin()
        {
            var users = _userRepository.GetUsersForAdmin();
            return users.Select(x => new UserDTO
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.Email,
                Username = x.Username,
                Role = x.Role.ToString()
            }).ToList();
            //return new List<UserDTO>
            //{
            //    Id = users.Id,
            //    FirstName = users.FirstName,
            //    LastName = users.LastName,
            //    Email = users.Email,
            //    Username = users.Username,
            //    Role = users.Role
            //};
        }
        //public string GenerateJwtToken(UserDTO user)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.UTF8.GetBytes("f75d32390e8354b5ff5b47261fc56b68fd81102ae7f9849f50dad7bff7d4c943753d60bc0a62a1408a191e88fb395cc2b06c245b634caa3715693248841cba163f27c1ee9c5d9f224826137017580e6d41d60ed3975e9b1bc21e18e9a41e2bff634d892b5d4cc54c974ae42206c32a2d433b93f107d30581531da41dec0eeb69e1ebaaad9da706390c68879bf614b423a38a755908a59810ade7d65970473d867cca00e1686660a80ba05e5b680f4ac8687401c45ed33813233d1f16bdf1a36b9540e7ac7a44e61d47427d9191d90b9a5d4bd90ac4d69af2d4d5d46bf52703c57ff7ea68a0d6f7114a5d4c5fed00f059503f8e8448dd5a8b6abb3db5568ed982"); // Mora se poklapati sa `appsettings.json`

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    new Claim(ClaimTypes.Name, user.Username),
        //    new Claim(ClaimTypes.Email, user.Email),
        //    new Claim(ClaimTypes.Role, user.Role.ToString()) // Dodajemo ulogu korisnika u token
        //}),
        //        Expires = DateTime.UtcNow.AddHours(2),
        //        Issuer = "http://localhost:5036/",
        //        Audience = "http://localhost:5036/",
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };

        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}

        //public string GenerateRefreshToken()
        //{
        //    var randomTokenNumber = new byte[32];
        //    using(var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomTokenNumber);
        //        return Convert.ToBase64String(randomTokenNumber);
        //    }
        //}
    }
}
