using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TaskBlog.Application.Interfaces;
using TaskBlog.Domain.Entities;

namespace TaskBlog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTasks(
        int pageNumber = 1,
        int pageSize = 10,
        string? searchTerm = null
    )
    {
        var tasks = await _taskService.GetAllTasksAsync(pageNumber, pageSize, searchTerm);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(Guid id, TaskItem task)
    {
        await _taskService.GetTaskByIdAsync(id);
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
    {
        var createdTask = await _taskService.CreateTaskAsync(task);
        return CreatedAtAction(nameof(GetAllTasks), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, [FromBody] TaskItem task)
    {
        await _taskService.UpdateTaskAsync(id, task);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskService.DeleteTaskAsync(id);
        return NoContent();
    }
}
