namespace TaskManagerApi.Api.Models;

/// <summary>
/// Search, filter, and sort options for querying tasks, in addition to paging.
/// </summary>
public class TaskFilterParams : PaginationParams
{
    /// <summary>Case-insensitive substring match against the task title.</summary>
    public string? Search { get; set; }

    /// <summary>Filter by completion status.</summary>
    public bool? IsCompleted { get; set; }

    /// <summary>Field to sort by: "id", "title", or "createdAt". Defaults to "id".</summary>
    public string? SortBy { get; set; }

    /// <summary>Only include tasks created after this UTC date/time.</summary>
    public DateTime? CreatedAfter { get; set; }

    /// <summary>Only include tasks created before this UTC date/time.</summary>
    public DateTime? CreatedBefore { get; set; }
}
