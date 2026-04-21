using TaskBlog.Domain.Entities;

namespace TaskBlog.Application.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllTasksAsync(int pageNumber, int pageSize, string? searchTerm);
    Task<TaskItem?> GetTaskByIdAsync(Guid id);
    Task<TaskItem> CreateTaskAsync(TaskItem task);
    Task UpdateTaskAsync(Guid id, TaskItem task);
    Task DeleteTaskAsync(Guid id);
}
