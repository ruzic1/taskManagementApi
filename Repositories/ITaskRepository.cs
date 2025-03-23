using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using TaskModel = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        List<TaskModel> GetTasks();
        List<User> GetUsersByCompany(int id);
        bool CreateTask(TaskModel task);
    }
}
