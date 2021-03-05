using System;
using System.Collections.Generic;
using System.Linq;

namespace LibShare.Api.Data.ApiModels.ResponseApiModels
{
    public class PagedListApiModel<T>
    {
        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public List<T> List { get; set; }
        public PagedListApiModel(List<T> items, int count, int pageNumber, int pageSize)
        {
            List = items;
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
        public static PagedListApiModel<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PagedListApiModel<T>(items, count, pageNumber, pageSize);
        }
    }
}
