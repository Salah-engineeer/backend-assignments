using System;
using System.Collections.Generic;
using System.Linq;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class TaskService
    {
        
        private readonly List<TaskItem> _tasks = new List<TaskItem>();

        private readonly Dictionary<string, Func<IEnumerable<TaskItem>, IOrderedEnumerable<TaskItem>>> _sortWhitelist =
            new Dictionary<string, Func<IEnumerable<TaskItem>, IOrderedEnumerable<TaskItem>>>(StringComparer.OrdinalIgnoreCase)
            {
                { "id", q => q.OrderBy(t => t.Id) },
                { "title", q => q.OrderBy(t => t.Title) },
                { "createdat", q => q.OrderBy(t => t.CreatedAt) }
            };

        public PagedResult<TaskItem> GetAll(TaskFilterParams filter)
        {
            var query = _tasks.AsEnumerable();

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

            if (_sortWhitelist.TryGetValue(sortKey, out var sortFunc))
            {
                query = sortFunc(query);
            }
            else
            {
               
                query = _sortWhitelist["id"](query);
            }

            
            var totalCount = query.Count();

            var pagedData = query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToList();

            return new PagedResult<TaskItem>(pagedData, totalCount, filter.Page, filter.PageSize);
        }
    }
}