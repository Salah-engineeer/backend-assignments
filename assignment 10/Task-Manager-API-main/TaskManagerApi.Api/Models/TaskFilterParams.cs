using System;

namespace TaskManager.API.Models
{
    public class TaskFilterParams : PaginationParams
    {
        public string? Search { get; set; }
        public bool? IsCompleted { get; set; }
        public string? SortBy { get; set; }

        // BONUS: Date Range Filters
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }
    }
}