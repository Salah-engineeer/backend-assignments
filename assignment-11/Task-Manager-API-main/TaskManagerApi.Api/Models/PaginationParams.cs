namespace TaskManagerApi.Api.Models;

/// <summary>
/// Common paging options shared by filterable list endpoints.
/// </summary>
public class PaginationParams
{
    private const int MaxPageSize = 100;
    private const int DefaultPageSize = 10;
    private int _pageSize = DefaultPageSize;
    private int _page = 1;

    /// <summary>The page number to retrieve (1-based). Values below 1 are treated as 1.</summary>
    public int Page
    {
        get => _page;
        set => _page = value < 1 ? 1 : value;
    }

    /// <summary>The number of items to return per page. Clamped between 1 and 100 (default 10).</summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? DefaultPageSize : (value > MaxPageSize ? MaxPageSize : value);
    }
}