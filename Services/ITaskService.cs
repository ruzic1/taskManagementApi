using TaskManagementAPI.DTO;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        List<TaskDTO> GetTasks();
        List<UserDTO> GetUsersByCompanyService(int userId);
        bool CreateTask(CreateTaskDTO taskDTO);
    }
}
