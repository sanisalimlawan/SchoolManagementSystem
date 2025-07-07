using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class PaginatedList<T> : List<T>
    {
        public int TotalPages { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalRecords {  get; private set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex> 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        private PaginatedList(IEnumerable<T> items, int count,int pageIndex, int PageSize)
        {
            PageIndex = pageIndex;
            TotalRecords = count;
            TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            this.AddRange(items);
        }
        public static PaginatedList<T> Create(List<T>? items, int count, FilterOptions filter)
        {
            return items is null ? new PaginatedList<T>(Enumerable.Empty<T>().ToList(), 0,0,0) : new (items,count,filter.PageIndex, filter.PageSize);
        } 
        
    }
}
