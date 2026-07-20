using TaskManagerApi.Api.Models;
using TaskManagerApi.Api.Models.Entities;
using TaskManagerApi.Api.Repositories.Interfaces;
using TaskManagerApi.Api.Services.Interfaces;

namespace TaskManagerApi.Api.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _taskRepository;

    private static readonly Dictionary<string, Func<IEnumerable<TaskItem>, IOrderedEnumerable<TaskItem>>> _sortWhitelist =
        new(StringComparer.OrdinalIgnoreCase)
        {
            { "id", q => q.OrderBy(t => t.Id) },
            { "title", q => q.OrderBy(t => t.Title) },
            { "createdat", q => q.OrderBy(t => t.CreatedAt) }
        };

    public TaskService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public List<TaskItem> GetAllTasks() => _taskRepository.GetAll();

    public TaskItem? GetTaskById(int id) => _taskRepository.GetById(id);

    public TaskItem CreateTask(string title)
    {
        var task = new TaskItem
        {
            Title = title,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };
        return _taskRepository.Add(task);
    }

    public TaskItem? UpdateTask(int id, string title, bool isCompleted)
    {
        var existing = _taskRepository.GetById(id);
        if (existing == null) return null;

        var updated = new TaskItem
        {
            Id = id,
            Title = title,
            IsCompleted = isCompleted,
            CreatedAt = existing.CreatedAt
        };

        _taskRepository.Update(updated);
        return updated;
    }

    public bool DeleteTask(int id) => _taskRepository.Delete(id);

    public PagedResult<TaskItem> GetTasks(TaskFilterParams filter)
    {
        var query = _taskRepository.GetAll().AsEnumerable();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            query = query.Where(t => t.Title.Contains(filter.Search, StringComparison.OrdinalIgnoreCase));
        }
        if (filter.IsCompleted.HasValue)
        {
            query = query.Where(t => t.IsCompleted == filter.IsCompleted.Value);
        }
        if (filter.CreatedAfter.HasValue)
        {
            query = query.Where(t => t.CreatedAt > filter.CreatedAfter.Value);
        }
        if (filter.CreatedBefore.HasValue)
        {
            query = query.Where(t => t.CreatedAt < filter.CreatedBefore.Value);
        }

        var sortKey = filter.SortBy ?? "id";
        query = _sortWhitelist.TryGetValue(sortKey, out var sortFunc)
            ? sortFunc(query)
            : _sortWhitelist["id"](query);

        var totalCount = query.Count();

        var pagedData = query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();

        return new PagedResult<TaskItem>(pagedData, totalCount, filter.Page, filter.PageSize);
    }
}
