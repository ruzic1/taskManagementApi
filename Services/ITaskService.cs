using TaskManagementAPI.DTO;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        List<TaskDTO> GetTasks();
        List<UserDTO> GetUsersByCompanyService(int userId);
        Task<bool> CreateTask(CreateTaskDTO taskDTO);
        Task<bool> DeleteTaskService(int taskId);
    }
}
