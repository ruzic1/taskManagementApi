﻿using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TaskManagementAPI.Db;
using TaskManagementAPI.Models;
using TaskModel = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Repositories
{
    public class TaskRepository:ITaskRepository
    {
        private readonly ApplicationDbContext _context;
        public TaskRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public List<TaskModel> GetTasks()
        {
            var tasks = _context.Tasks.Select(x => new TaskModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                DueDate = x.DueDate,
                //UserName=x.User.Username,
                //Email=x.User.Email,
                //UserId=x.User.Id,
                TaskPriority=x.TaskPriority,
                TaskStatus=x.TaskStatus
            }).ToList();

            return tasks;
        }

        public List<User> GetUsersByCompany(int _userId)
        {
            var result = _context.Users.Find(_userId);
            var abc = result.CompanyId;
            var users = _context.Users.Where(y=>y.CompanyId==abc).Select(x => new User
            {
                Id = x.Id,
                FirstName=x.FirstName,
                LastName=x.LastName,
                Email = x.Email,
                Username = x.Username,
                Role=x.Role

            }).ToList();
            return users;
            //
        }
        //public bool CreateTask(TaskModel task)
        //{
        //    if (task==null)
        //    {          
        //        return false;
        //    }
        //    else
        //    {

        //        _context.Add(task);
        //        _context.SaveChanges();
        //        return true;
        //    }
        //}
        public async Task<bool> AddTaskAsync(TaskModel task)
        {
            try
            {
                await _context.Tasks.AddAsync(task);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> AddTaskAssignmentAsync(TaskAssignment taskAssignment)
        {
            try
            {
                await _context.TaskAssignments.AddAsync(taskAssignment);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<bool> DeleteTask(int id){
            try
            {
                var task=await _context.Tasks.FirstOrDefaultAsync(x=>x.Id==id);

                if(task==null){
                    throw new Exception("Task not found");
                }
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
                return true;

            }catch(Exception ex){
                throw new Exception("Error: [message] - "+ex.Message);
            }
        }
    }
}
