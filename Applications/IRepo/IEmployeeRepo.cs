using Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IEmployeeRepo : IBaseRepo<EmployeeViewModel>
    {
        public Task<BaseResponse> CreateAsync(EmployeeViewModel model,IFormFile file);
        public Task<BaseResponse> UpdateAsync(EmployeeViewModel model, IFormFile file);
        public Task<EmployeeViewModel> GetByUserId(Guid UserId);
        public Task<PaginatedList<EmployeeViewModel>> GetTeachersAsync(FilterOptions filter);
    }
}
