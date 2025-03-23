namespace TaskManagementAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description {  get; set; }
        public DateTime CreatedAt { get; set; }=DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority TaskPriority { get; set; } = TaskPriority.Medium;
        public TaskStatus TaskStatus { get; set; } = TaskStatus.ToDo;
        //public string 

        //foreign keys
        //public int UserId { get; set; }
        //public User User { get; set; }

        //public string UserName { get; set; }
        //public string Email { get; set; }
        //public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public ICollection<TaskAssignment> TaskAssignments { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
