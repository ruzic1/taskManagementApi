namespace TaskManagementAPI.DTO
{
    public class TaskDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string TaskPriority { get; set; }
        public string TaskStatus { get; set; }

        //public int UserId { get; set; }
        //public string Username { get; set; }
        //public string Email { get; set; }
        //public int CompanyId { get; set; }


        //public TaskPriority TaskPriority { get; set; }
    }
}
