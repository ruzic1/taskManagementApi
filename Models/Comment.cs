﻿namespace TaskManagementAPI.Models
{
    public class Comment
    {
        public int Id { get; set; }


        public int TaskId {  get; set; }
        public Task Task { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
