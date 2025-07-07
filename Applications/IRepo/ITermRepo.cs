
using Application.IRepo;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepo
{
    public interface ITermRepo : IBaseRepo<TermViewModel>
    {
        public Task<BaseResponse> CreateAsync(string name, DateOnly startDate, DateOnly endDate, Guid Sessid);
        public Task<BaseResponse> UpdateAsync(Guid id, string name, DateOnly startDate, DateOnly endDate, Guid Sessid);
    }
}
