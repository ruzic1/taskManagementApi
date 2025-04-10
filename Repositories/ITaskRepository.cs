using TaskManagementAPI.DTO;
using TaskManagementAPI.Models;
using TaskModel = TaskManagementAPI.Models.Task;

namespace TaskManagementAPI.Repositories
{
    public interface ITaskRepository
    {
        List<TaskModel> GetTasks();
        List<User> GetUsersByCompany(int id);
        //bool CreateTask(TaskModel task);
        Task<bool> AddTaskAsync(TaskModel task);
        Task<bool> AddTaskAssignmentAsync(TaskAssignment taskAssignment);

        Task<bool> DeleteTask(int id);
    }
}
