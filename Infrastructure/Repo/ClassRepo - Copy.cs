using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class IncomeRepo : IIncomeRepo
    {
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        public IncomeRepo(SchoolDbContext db,UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<BaseResponse> CreateAsync(IncomeViewModel item, string createdBy)
        {
            //var validate = await _db.incomes.SingleOrDefaultAsync(x => x.Name == item.Name);
            //if(validate != null)
            //{
            //    return new BaseResponse() { Status = false, Message = "Income with same name Exist" };
            //}
            var incomes = new Income
            {
                Id = Guid.NewGuid(),
                description = item.Description,
                Amount = item.Amount,
                Date = item.Date,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.incomes.Add(incomes);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Income created successfuly" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var clIndb = await _db.incomes.FindAsync(id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "income Not Found!" };
            _db.incomes.Remove(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "income Deleted Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public Task<IEnumerable<IncomeViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IncomeViewModel?> GetByIdAsync(Guid id)
        {
            var incomevm = await _db.incomes.Select(x => new IncomeViewModel
            {
                Id = x.Id,
                Description = x.description,
                Amount = x.Amount,
                Date = x.Date,
            }).SingleOrDefaultAsync(x => x.Id == id);

            return incomevm;
        }

        public async Task<PaginatedList<IncomeViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.incomes.Select(x => new IncomeViewModel
            {
                Id = x.Id,

            });
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize).
                Take(filter.PageSize).ToListAsync();

            
            return PaginatedList<IncomeViewModel>.Create(items, count, filter);
        }

        public async Task<BaseResponse> UpdateAsync(IncomeViewModel item, string updatedby)
        {
            var clIndb = await _db.incomes.FindAsync(item.Id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "Income not Found" };
            clIndb.description = item.Description;
            clIndb.Date = item.Date;
            clIndb.Amount = item.Amount;
            clIndb.UpdatedAt = DateTime.UtcNow;
            _db.incomes.Update(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Income Update Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had serve error!" };

        }
    }
}
