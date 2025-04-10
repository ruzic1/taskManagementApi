using TaskManagementAPI.Db;
using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using TaskManagementAPI.Repositories;
using Xunit.Sdk;
using TaskModel = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Services
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ApplicationDbContext _context;
        public TaskService(ITaskRepository taskRepParam, ApplicationDbContext dbContext)
        {
            _taskRepository = taskRepParam;
            _context= dbContext;
        }
        public List<TaskDTO> GetTasks()
        {
            var callToRep = _taskRepository.GetTasks();
            if(callToRep == null)
            {
                return null;
            }
            return callToRep.Select(x => new TaskDTO
            {
                Id=x.Id,
                Description = x.Description,
                Title=x.Title,
                DueDate = x.DueDate,
                //UserId=x.UserId,
                //Username=x.UserName,
                //Email = x.Email,
                TaskPriority =x.TaskPriority.ToString(),
                TaskStatus=x.TaskStatus.ToString()
            }).ToList();
            //return "";
        }

        public List<UserDTO> GetUsersByCompanyService(int userid)
        {
            var callForRep = _taskRepository.GetUsersByCompany(userid);
            return callForRep.Select(x=>new UserDTO
            {
                Id=x.Id,
                FirstName = x.FirstName,
                LastName=x.LastName,
                Email =x.Email,
                Username=x.Username,
                Role=x.Role.ToString()
            }).ToList();       
        }

        public async Task<bool> CreateTask(CreateTaskDTO task)
        {
            using(var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //_context.Tasks.Add(task);
                    var newTask = new TaskModel
                    {
                        Title = task.Title,
                        Description = task.Description,
                        DueDate = task.DueDate,
                        
                    };
                    await _taskRepository.AddTaskAsync(newTask);
                    //await _context.SaveChangesAsync();

                    var taskAssignment = new TaskAssignment
                    {
                        TaskId = newTask.Id,
                        UserId = task.UserId,
                    };
                    await _taskRepository.AddTaskAssignmentAsync(taskAssignment);
                    //await _context.SaveChangesAsync();
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    
                    return true;
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new Exception($"Greška prilikom čuvanja zadatka: {ex.Message}", ex);
                }
            }


            //var newTask = new TaskModel
            //{
            //    Title = task.Title,
            //    Description = task.Description,
            //    DueDate = task.DueDate,
                
            //    TaskPriority = TaskPriority.Medium,
            //    TaskStatus = TaskStatus.ToDo
            //};
            //var _callForRep = _taskRepository.CreateTask(newTask);
            //if (_callForRep)
            //{
            //    return true;
            //}
            //else return false;
        }

        public async Task<bool> DeleteTaskService(int id)
        {
            var delete=await _taskRepository.DeleteTask(id);
            return delete?true:false;
            // if(delete==null){
            //     throw new Exception("")
            // }
        }
    }
}
