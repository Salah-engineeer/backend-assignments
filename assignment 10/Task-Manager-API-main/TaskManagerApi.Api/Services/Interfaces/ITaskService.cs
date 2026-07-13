namespace TaskManagerApi.Api.Services.Interfaces;

public interface ITaskService
{
      List<TaskItem> GetAllTasks();
      TaskItem? GetTaskById(int id);
      TaskItem CreateTask(string title);
}