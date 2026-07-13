using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaskManager.API.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;

        [JsonIgnore]
        public int CurrentPage { get; set; }

        public PagedResult(IEnumerable<T> data, int totalCount, int page, int pageSize)
        {
            Data = data;
            TotalCount = totalCount;
            CurrentPage = page;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        }
    }
}