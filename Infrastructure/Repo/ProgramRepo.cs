
using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repo
{
    public class ProgramRepo : IProgramRepo
    {
        private readonly SchoolDbContext _context;
        //private readonly IWebHostEnviroment
        public ProgramRepo(SchoolDbContext context)
        {
            _context = context;
        } 
        public async Task<BaseResponse> CreateAsync(string name,  string description)
        {
            var program = new Program
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
                Name = name,
                Description = description,
            };

            await _context.Programs.AddAsync(program);
            var result = await _context.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Program Added Successfully" };
            return new BaseResponse() { Status = false, Message = "You are unable to create Program" };
        }

        public Task<BaseResponse> CreateAsync(ProgramViewModel item, string createdBy)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var check = await _context.Programs.FindAsync(id);
            if(check != null)
            {
                 _context.Programs.Remove(check);
                var result = await _context.TrySaveChangesAsync();
                if (result) 
                    return new BaseResponse() { Status = true, Message = "Program Remove Successfully" };
                return new BaseResponse() { Status = false, Message = "Unable to delete remove Program" };
            }
            return new BaseResponse() { Status = false, Message = "Program NOt Found" };
        }

        public Task<IEnumerable<ProgramViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ProgramViewModel> GetByIdAsync(Guid id)
        {
            var check = await _context.Programs.SingleOrDefaultAsync(x => x.Id == id);
            if (check == null)
                return null;
            var program = new ProgramViewModel
            {
                Id = check.Id,
                Name = check.Name,
                Description = check.Description,
            };
            return program;
        }

        public async Task<PaginatedList<ProgramViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var programIndb = _context.Programs.Select(x => new ProgramViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });

            var count = await programIndb.CountAsync();
            var items = await programIndb.Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            return PaginatedList<ProgramViewModel>.Create(items, count, filter);
        }

        public async Task<BaseResponse> UpdateAsync(Guid id, string name,  string description)
        {
            var programIndb = await _context.Programs.FindAsync(id);
            if (programIndb == null)
                return new BaseResponse() { Status = false, Message = "Program Not Found" };
            programIndb.Id = id;
            programIndb.Name = name;
            programIndb.Description = description;
            _context.Programs.Update(programIndb);
            var result = await _context.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Program Updated Successfully" };
            return new BaseResponse() { Status = false, Message = "Uable to Update Program" };
        }

        public Task<BaseResponse> UpdateAsync(ProgramViewModel item, string updatedby)
        {
            throw new NotImplementedException();
        }
    }
}
