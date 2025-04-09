using TaskManagementServices.Domain;

namespace TaskManagementServices.Repositories.Interface
{
    public interface ITaskRepository
    {
        Task<TaskManagementServices.Domain.Task?> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskManagementServices.Domain.Task>> GetAllTasksAsync(int page, int pageSize, int userId, string sortBy, string sortOrder);
        Task<TaskManagementServices.Domain.Task> AddTaskAsync(TaskManagementServices.Domain.Task task);
        System.Threading.Tasks.Task<int> UpdateTaskAsync(int id, TaskManagementServices.Domain.Task task);
        System.Threading.Tasks.Task DeleteTaskAsync(int taskId);
    }
}
