namespace TaskManagementAPI.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Company_Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
    }
}
