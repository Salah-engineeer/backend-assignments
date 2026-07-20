using TaskManagerApi.Api.Models;
using TaskManagerApi.Api.Models.Entities;

namespace TaskManagerApi.Api.Services.Interfaces;

public interface ITaskService
{
    List<TaskItem> GetAllTasks();
    PagedResult<TaskItem> GetTasks(TaskFilterParams filter);
    TaskItem? GetTaskById(int id);
    TaskItem CreateTask(string title);
    TaskItem? UpdateTask(int id, string title, bool isCompleted);
    bool DeleteTask(int id);
}
