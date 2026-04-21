using TaskBlog.Application.Interfaces;
using TaskBlog.Domain.Entities;

namespace TaskBlog.Application.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllTasksAsync(
        int pageNumber,
        int pageSize,
        string? searchTerm
    )
    {
        return await _taskRepository.GetAllTasksAsync(pageNumber, pageSize, searchTerm);
    }

    public async Task<TaskItem?> GetTaskByIdAsync(Guid id)
    {
        return await _taskRepository.GetTaskByIdAsync(id);
    }

    public async Task<TaskItem> CreateTaskAsync(TaskItem task)
    {
        task.Id = Guid.NewGuid();
        task.CreatedAt = DateTime.UtcNow;
        await _taskRepository.AddTaskAsync(task);
        return task;
    }

    public async Task UpdateTaskAsync(Guid id, TaskItem updateTask)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);

        if (existingTask == null)
            throw new KeyNotFoundException("Task not found");

        existingTask.Title = updateTask.Title;
        existingTask.Description = updateTask.Description;
        existingTask.Status = updateTask.Status;

        await _taskRepository.UpdateTaskAsync(existingTask);
    }

    public async Task DeleteTaskAsync(Guid id)
    {
        var existingTask = await _taskRepository.GetTaskByIdAsync(id);

        if (existingTask == null)
            throw new KeyNotFoundException("Task not found");

        await _taskRepository.DeleteTaskAsync(existingTask);
    }
}
