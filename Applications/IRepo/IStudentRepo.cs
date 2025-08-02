using Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IStudentRepo : IBaseRepo<StudentViewModel>
    {
        public Task<BaseResponse> CreateAsync(StudentViewModel model,IFormFile file);
        public Task<BaseResponse> UpdateAsync(StudentViewModel model, IFormFile file);
        public Task<StudentViewModel> GetByUserId(Guid UserId);
        public Task<PaginatedList<StudentViewModel>> GetTeachersAsync(FilterOptions filter);
    }
}
