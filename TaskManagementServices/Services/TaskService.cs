using TaskManagementServices.DTOs;
using TaskManagementServices.Enums;
using TaskManagementServices.Repositories.Interface;
using TaskManagementServices.Services.Interface;

namespace TaskManagementServices.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ILogger<TaskService> _logger;

        public TaskService(ITaskRepository taskRepository, ILogger<TaskService> logger)
        {
            _taskRepository = taskRepository;
            _logger = logger;
        }

        public async Task<TaskModel> AddTaskAsync(int userId, TaskUpsertModel taskDto)
        {
            try
            {
                var task = MapToDomain(userId, taskDto);

                var createdTask = await _taskRepository.AddTaskAsync(task);
                return MapToDto(createdTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding task with title {title}", taskDto.Title);
                throw;
            }
        }

        public async System.Threading.Tasks.Task DeleteTaskAsync(int taskId)
        {
            await _taskRepository.DeleteTaskAsync(taskId);
        }

        public async Task<IEnumerable<TaskModel>> GetAllTasksAsync(int page, int pageSize, int userId, string sortBy, string sortOrder)
        {
            try
            {
                var tasks = await _taskRepository.GetAllTasksAsync(page, pageSize, userId, sortBy, sortOrder);
                return tasks.Select(MapToDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting all tasks for user with id {id}", userId);
                throw;
            }
        }

        public async Task<TaskModel?> GetTaskByIdAsync(int taskId)
        {
            try
            {
                var task = await _taskRepository.GetTaskByIdAsync(taskId);
                return task is null ? null : MapToDto(task);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting task by id {id}", taskId);
                throw;
            }
        }

        public async Task<int?> UpdateTaskAsync(int id, TaskUpsertModel dto)
        {
            try
            {
                var entity = await _taskRepository.GetTaskByIdAsync(id);
                if (entity == null) return null;

                entity = MapToUpdateDomain(entity, dto);

                var result = await _taskRepository.UpdateTaskAsync(id, entity);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating task by id {id}", id);
                throw;
            }
        }

        private TaskManagementServices.Domain.Task MapToDomain(int userId, TaskUpsertModel model)
        {
            return new TaskManagementServices.Domain.Task
            {
                Title = model.Title,
                Description = model.Description,
                DueDate = model.DueDate.ToUniversalTime(),
                StatusId = (int)model.Status,
                UserId = userId
            };
        }

        private TaskManagementServices.Domain.Task MapToUpdateDomain(TaskManagementServices.Domain.Task entity, TaskUpsertModel updateModel)
        {
            entity.Title = updateModel.Title;
            entity.Description = updateModel.Description;
            entity.DueDate = updateModel.DueDate.ToUniversalTime();
            entity.StatusId = (int)updateModel.Status;
            return entity;
        }

        private TaskModel MapToDto(TaskManagementServices.Domain.Task task)
        {
            var statusEnum = (StatusEnum)task.StatusId;

            return new TaskModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                Status = statusEnum.GetDescription() 
            };
        }
    }
}
