
using Microsoft.EntityFrameworkCore;

namespace Fullstack.Persistence.Models
{
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PageList(){}
        public PageList (List<T> items,int count,int PageNumber,int pageSize) { 
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = PageNumber;
            TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            AddRange(items);
        }

        public static async Task<PageList<T>> CreateAsync(
            IQueryable<T> source,int pageNumber,int pageSize
        ){
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber-1)*pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items,count,pageNumber,pageSize);
        }
    }
}