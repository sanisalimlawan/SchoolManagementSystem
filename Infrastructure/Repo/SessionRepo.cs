using Application.ViewModels;
using Core.IRepo;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
    public class SessionRepo : ISessionRepo
    {
        private readonly SchoolDbContext _db;
        public SessionRepo(SchoolDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse> CreateAsync(string name, DateOnly startDate, DateOnly endDate)
        {
            //x.EndDate == endDate ||
            var check = await _db.Sessions.AnyAsync(x => x.EndDate >=   startDate  ||  x.Name == name );
            if (check)
            {
                return new BaseResponse() { Status = false, Message = "Session with Name or StartDate or EndDate Exist" };
            }
            //var sterche = await _db.Sessions.AnyAsync(x => startDate <= x.StartDate);
            if (endDate <= startDate)
            {
                return new BaseResponse() { Status = false, Message = "End Date can not be less than start Date" };
            }
            var session = new Session
            {
                Id = Guid.NewGuid(),
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,

            };

            _db.Sessions.Add(session);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Session Created successufully" };
            return new BaseResponse() { Status = false, Message = "Server Error!" };

        }

        public Task<BaseResponse> CreateAsync(SessionViewModel item, string createdby)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var check = await _db.Sessions.FindAsync(id);
            if (check == null)
            {
                return new BaseResponse() { Status = false, Message = "session not found in the Db" };
            }
            _db.Sessions.Remove(check);
            var result = await _db.TrySaveChangesAsync();
            if (result) return new BaseResponse() { Status = true, Message = "session Remove Successufully" };
            return new BaseResponse() { Status = false, Message = "server Error" };
        }

        public Task<IEnumerable<SessionViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SessionViewModel?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<SessionViewModel>> GetPaginatedList(FilterOptions options)
        {
            var data = _db.Sessions.Select(x => new SessionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
            });

            var count = await data.CountAsync();
            var query = data.Where(c => string.IsNullOrEmpty(options.Query) || c.Name.Contains(options.Query));
            var items = await data.Skip((options.PageIndex - 1) * options.PageSize)
                .Take(options.PageSize).ToListAsync();
            return PaginatedList<SessionViewModel>.Create(items, count, options);
        }

        public async Task<BaseResponse> UpdateAsync(Guid id, string name, DateOnly startDate, DateOnly endDate)
        {
            var check = await _db.Sessions.SingleOrDefaultAsync(x => x.Id == id);
            if (check == null)
            {
                return new BaseResponse() { Status = false, Message = "Session Not Found" };
            }
            check.Name = name;
            check.StartDate = startDate;
            check.EndDate = endDate;
            _db.Sessions.Update(check);
            var result = await _db.TrySaveChangesAsync();
            if (result) return new BaseResponse() { Status = true, Message = "session Updated Successufully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error" };
        }

        public Task<BaseResponse> UpdateAsync(SessionViewModel item, string Updatedby)
        {
            throw new NotImplementedException();
        }
    }
}
