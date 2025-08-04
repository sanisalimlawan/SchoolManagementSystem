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
    public class ExpensiveRepo : IExpensiveRepo
    {
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        public ExpensiveRepo(SchoolDbContext db,UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<BaseResponse> CreateAsync(ExpensiveViewModel item, string createdBy)
        {
            //var validate = await _db.incomes.SingleOrDefaultAsync(x => x.Name == item.Name);
            //if(validate != null)
            //{
            //    return new BaseResponse() { Status = false, Message = "Income with same name Exist" };
            //}
            var expensive = new Expensive
            {
                Id = Guid.NewGuid(),
                description = item.Description,
                Amount = item.Amount,
                DateSpent = item.DateSpent,
                Category = item.Category,
                ApprovedByUserId = item.ApprovedByUserId,
                PaymentMethod = item.PaymentMethod,
                Vendor = item.Vendor,
                ReferenceNumber = item.ReferenceNumber,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.expensives.Add(expensive);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Expensive Added successfuly" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var clIndb = await _db.expensives.FindAsync(id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "Expensive Not Found!" };
            _db.expensives.Remove(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Expensive Deleted Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public Task<IEnumerable<ExpensiveViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ExpensiveViewModel?> GetByIdAsync(Guid id)
        {
            var expenvm = await _db.expensives.Select(x => new ExpensiveViewModel
            {
                Id = x.Id,
                Description = x.description,
                Amount = x.Amount,
                DateSpent = x.DateSpent,
                ApprovedByUserId = x.ApprovedByUserId,
                Category = x.Category,
                PaymentMethod = x.PaymentMethod,
                Vendor = x.Vendor,
                ReferenceNumber = x.ReferenceNumber,
                
            }).SingleOrDefaultAsync(x => x.Id == id);

            return expenvm;
        }

        public async Task<PaginatedList<ExpensiveViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.expensives.Select(x => new ExpensiveViewModel
            {
                Id = x.Id,
                Description = x.description,
                Amount = x.Amount,
                DateSpent = x.DateSpent,
                ApprovedByUserId = x.ApprovedByUserId,
                Category = x.Category,
                PaymentMethod = x.PaymentMethod,
                Vendor = x.Vendor,
                ReferenceNumber = x.ReferenceNumber,

            });
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize).
                Take(filter.PageSize).ToListAsync();

            
            return PaginatedList<ExpensiveViewModel>.Create(items, count, filter);
        }

        public async Task<BaseResponse> UpdateAsync(ExpensiveViewModel item, string updatedby)
        {
            var clIndb = await _db.expensives.FindAsync(item.Id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "Expensive not Found" };
            clIndb.description = item.Description;
            clIndb.DateSpent = item.DateSpent;
            clIndb.ApprovedByUserId = item.ApprovedByUserId;
            clIndb.Category = item.Category;
            clIndb.Vendor = item.Vendor;
            clIndb.ReferenceNumber = item.ReferenceNumber;
            clIndb.PaymentMethod = item.PaymentMethod;
            clIndb.Amount = item.Amount;
            clIndb.UpdatedAt = DateTime.UtcNow;
            _db.expensives.Update(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Expensive Update Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had serve error!" };

        }
    }
}
