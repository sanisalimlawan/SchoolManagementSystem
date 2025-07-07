using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IBaseRepo<T>
    {
        public Task<BaseResponse> CreateAsync(T item, string createdBy);
        public Task<BaseResponse> UpdateAsync(T item, string updatedby);
        public Task<BaseResponse> DeleteAsync(Guid id);
        public Task<PaginatedList<T>> GetPaginatedList(FilterOptions filter);
        public Task<T?> GetByIdAsync(Guid id);
        public Task<IEnumerable<T>> GetAllAsync();
    }
}
