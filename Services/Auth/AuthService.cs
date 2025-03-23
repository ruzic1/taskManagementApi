using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;

namespace TaskManagementAPI.Services.Auth
{
    public class AuthService:IAuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration config)
        {
            _configuration = config;
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),   // ID korisnika
                new Claim(JwtRegisteredClaimNames.UniqueName, user.Username), // Korisničko ime
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpirationMinutes"])),
                signingCredentials:credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
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
        }

        public string GenerateRefreshToken()
        {
            var randomTokenNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomTokenNumber);
                //return Convert.ToBase64String(randomTokenNumber);
            }
            return Convert.ToBase64String(randomTokenNumber);
        }
    }
}
