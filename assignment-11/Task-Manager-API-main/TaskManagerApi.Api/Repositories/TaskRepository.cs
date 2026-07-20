using TaskManagerApi.Api.Models.Entities;
using TaskManagerApi.Api.Repositories.Interfaces;

namespace TaskManagerApi.Api.Repositories;

public class TaskRepository : ITaskRepository
{
    private static readonly List<TaskItem> _tasks = new()
    {
        new TaskItem { Id = 1, Title = "Buy Groceries", IsCompleted = false, CreatedAt = DateTime.UtcNow },
        new TaskItem { Id = 2, Title = "Read a Book", IsCompleted = true, CreatedAt = DateTime.UtcNow },
    };

    private static int _nextId = 3;

    public List<TaskItem> GetAll() => _tasks;

    public TaskItem? GetById(int id) => _tasks.FirstOrDefault(t => t.Id == id);

    public TaskItem Add(TaskItem task)
    {
        task.Id = _nextId++;
        _tasks.Add(task);
        return task;
    }

    public bool Update(TaskItem task)
    {
        var existing = GetById(task.Id);
        if (existing == null) return false;

        existing.Title = task.Title;
        existing.IsCompleted = task.IsCompleted;
        return true;
    }

    public bool Delete(int id)
    {
        var existing = GetById(id);
        if (existing == null) return false;

        _tasks.Remove(existing);
        return true;
    }
}
