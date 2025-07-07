using Application.IRepo;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepo
{
    public interface IProgramRepo : IBaseRepo<ProgramViewModel>
    {
        public Task<BaseResponse> CreateAsync(string name,  string description);
        public Task<BaseResponse> UpdateAsync(Guid id, string name,  string description);
    }
}
