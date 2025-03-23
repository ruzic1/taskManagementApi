using TaskManagementAPI.DTO;
using TaskManagementAPI.Repositories;
using TaskModel = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Services
{
    public class TaskService:ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepParam)
        {
            _taskRepository = taskRepParam;
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

        public bool CreateTask(CreateTaskDTO task)
        {
            var newTask = new TaskModel
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                //UserId = task.UserId,
                TaskPriority = TaskPriority.Medium,
                TaskStatus = TaskStatus.ToDo
            };
            var _callForRep = _taskRepository.CreateTask(newTask);
            if (_callForRep)
            {
                return true;
            }
            else return false;
        }

    }
}
