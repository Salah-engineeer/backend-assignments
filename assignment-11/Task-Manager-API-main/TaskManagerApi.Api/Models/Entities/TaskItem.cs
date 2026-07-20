namespace TaskManagerApi.Api.Models.Entities;

/// <summary>
/// A single task tracked by the API.
/// </summary>
public class TaskItem
{
    /// <summary>The task's unique id. Assigned by the server; ignored on create.</summary>
    public int Id { get; set; }

    /// <summary>The task's title. Required.</summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>Whether the task has been completed.</summary>
    public bool IsCompleted { get; set; }

    /// <summary>When the task was created, in UTC. Assigned by the server; ignored on create.</summary>
    public DateTime CreatedAt { get; set; }
}
