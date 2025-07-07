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
    public class TermRepo : ITermRepo
    {
        private readonly SchoolDbContext _db;
        public TermRepo(SchoolDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse> CreateAsync(string name, DateOnly startDate, DateOnly endDate, Guid Sessid)
        {
            // Conflix checking before creating
            var check = await _db.Terms.AnyAsync(x => x.EndDate >= startDate || x.Name == name);
            if (check)
            {
                return new BaseResponse() { Status = false, Message = "Session with Name or StartDate or EndDate Already Exist" };
            }
            if (endDate <= startDate)
            {
                return new BaseResponse() { Status = false, Message = "End Date can not be less than start Date" };
            }
            var Term = new Term
            {
                Id = Guid.NewGuid(),
                Name = name,
                StartDate = startDate,
                EndDate = endDate,
                sessionId = Sessid,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,

            };

            _db.Terms.Add(Term);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Term Created successfully" };
            return new BaseResponse() { Status = false, Message = "Server Error!" };

        }

        public Task<BaseResponse> CreateAsync(TermViewModel item, string createdby)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var check = await _db.Terms.FindAsync(id);
            if (check == null)
            {
                return new BaseResponse() { Status = false, Message = "Term not found!" };
            }
            _db.Terms.Remove(check);
            var result = await _db.TrySaveChangesAsync();
            if (result) return new BaseResponse() { Status = true, Message = "Term Remove Successfully" };
            return new BaseResponse() { Status = false, Message = "server Error" };
        }

        public Task<IEnumerable<TermViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TermViewModel?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<TermViewModel>> GetPaginatedList(FilterOptions options)
        {
            var data = _db.Terms.Select(x => new TermViewModel
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                session = new SessionViewModel
                {
                    Id = x.session.Id,
                    Name = x.session.Name,
                    StartDate = x.session.StartDate,
                    EndDate = x.session.EndDate,
                }
            });

            var count = await data.CountAsync();
            var query = data.Where(c => string.IsNullOrEmpty(options.Query) || c.Name.Contains(options.Query));
            var items = await data.Skip((options.PageIndex - 1) * options.PageSize)
                .Take(options.PageSize).ToListAsync();
            return PaginatedList<TermViewModel>.Create(items, count, options);
        }

        public async Task<BaseResponse> UpdateAsync(Guid id, string name, DateOnly startDate, DateOnly endDate, Guid Sessid)
        {
            var check = await _db.Terms.SingleOrDefaultAsync(x => x.Id == id);
            if (check == null)
            {
                return new BaseResponse() { Status = false, Message = "Term Not Found" };
            }
            // conflix checking before updating
            //var checkconflix = await _db.Terms.AnyAsync(x => x.EndDate >= startDate);
            //if (checkconflix)
            //{
            //    return new BaseResponse() { Status = false, Message = "Session with Name or StartDate or EndDate Already Exist" };
            //}
            //if (endDate <= startDate)
            //{
            //    return new BaseResponse() { Status = false, Message = "End Date can not be less than start Date" };
            //}
            check.Name = name;
            check.StartDate = startDate;
            check.EndDate = endDate;
            check.sessionId = Sessid;
            _db.Terms.Update(check);
            var result = await _db.TrySaveChangesAsync();
            if (result) return new BaseResponse() { Status = true, Message = "Term Updated Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error" };
        }

        public Task<BaseResponse> UpdateAsync(TermViewModel item, string Updatedby)
        {
            throw new NotImplementedException();
        }
    }
}
