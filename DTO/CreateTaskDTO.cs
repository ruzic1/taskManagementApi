namespace TaskManagementAPI.DTO
{
    public class CreateTaskDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public int UserId { get; set; }
        public DateTime DueDate { get; set; }

        //public string Email { get; set; }

    }
}
