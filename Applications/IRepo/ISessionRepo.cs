using Application.IRepo;
using Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.IRepo
{
    public interface ISessionRepo : IBaseRepo<SessionViewModel>
    {
        public Task<BaseResponse> CreateAsync(string name, DateOnly startDate, DateOnly endDate);
        public Task<BaseResponse> UpdateAsync(Guid id, string name, DateOnly startDate, DateOnly endDate);
    }
}
