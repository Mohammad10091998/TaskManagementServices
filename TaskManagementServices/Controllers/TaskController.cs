using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementServices.Auth;
using TaskManagementServices.DTOs;
using TaskManagementServices.Services.Interface;

namespace TaskManagementAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly UserContext _userContext;

        public TaskController(ITaskService taskService, UserContext userContext)
        {
            _taskService = taskService;
            _userContext = userContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetTasks(int page =1, int pageSize = 5, string sortBy = "duedate", string sortOrder = "asc")
        {
            var tasks = await _taskService.GetAllTasksAsync(page, pageSize, _userContext.UserId, sortBy, sortOrder);
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskUpsertModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var createdTask = await _taskService.AddTaskAsync(_userContext.UserId, model);
            return CreatedAtAction(nameof(GetTaskById), new { id = createdTask.Id }, createdTask);
        }

        // PUT: api/tasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskUpsertModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var updatedTask = await _taskService.UpdateTaskAsync(id, model);
            if (updatedTask == null) return NotFound();
            return Ok(updatedTask);
        }

        // DELETE: api/tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
