using Microsoft.EntityFrameworkCore;
using TaskManagementServices.Data;
using TaskManagementServices.Repositories.Interface;

namespace TaskManagementServices.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskManagementServices.Domain.Task?> GetTaskByIdAsync(int taskId)
        {
            return await _context.Tasks
                .FirstOrDefaultAsync(t => t.Id == taskId);
        }

        public async Task<IEnumerable<TaskManagementServices.Domain.Task>> GetAllTasksAsync(int page, int pageSize, int userId, string sortBy,string sortOrder)
        {
            var query = _context.Tasks.Where(t => t.UserId == userId);

            query = (sortBy.ToLower(), sortOrder.ToLower()) switch
            {
                ("duedate", "asc") => query.OrderBy(t => t.DueDate),
                ("duedate", "desc") => query.OrderByDescending(t => t.DueDate),
                ("status", "asc") => query.OrderBy(t => t.StatusId),
                ("status", "desc") => query.OrderByDescending(t => t.StatusId),
                _ => query.OrderBy(t => t.DueDate) 
            };

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<TaskManagementServices.Domain.Task> AddTaskAsync(TaskManagementServices.Domain.Task task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<int> UpdateTaskAsync(int id, TaskManagementServices.Domain.Task updateTask)
        {
            _context.Tasks.Update(updateTask);
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
