using TaskManagementServices.DTOs;

namespace TaskManagementServices.Services.Interface
{
    public interface ITaskService
    {
        Task<TaskModel> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskModel>> GetAllTasksAsync(int page, int pageSize, int userId, string sortBy, string sortOrder);
        Task<TaskModel> AddTaskAsync(int userId, TaskUpsertModel task);
        System.Threading.Tasks.Task<int?> UpdateTaskAsync(int id, TaskUpsertModel task);
        System.Threading.Tasks.Task DeleteTaskAsync(int taskId);
    }
}
