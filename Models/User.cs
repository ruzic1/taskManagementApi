namespace TaskManagementAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public UserRole Role { get; set; }=UserRole.User;
        public string LastName { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }


        public int? CompanyId { get; set; }
        public Company Company { get; set; }

        //public List<Task> Tasks { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
