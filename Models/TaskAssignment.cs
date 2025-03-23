namespace TaskManagementAPI.Models
{
    public class TaskAssignment
    {
        //public int Id { get; set; }
        //public string Name { get; set; }
        public int Id { get; set; }
        //public int TempColumn { get; set; }
        //public int UserId { get; set; }
        //public int TaskId { get; set; }
        //public string TaskAssignmentName { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }


        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
