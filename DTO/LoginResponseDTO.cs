﻿namespace TaskManagementAPI.DTO
{
    public class LoginResponseDTO
    {
        public string JwtToken { get; set; }
        public string RefreshToken{ get; set; }
    }
}
