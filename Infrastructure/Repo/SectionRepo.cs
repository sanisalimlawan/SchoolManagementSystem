using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class SectionRepo : ISectionRepo
    {
        private readonly SchoolDbContext _db;
        public SectionRepo(SchoolDbContext db)
        {
            _db = db;
        }
        public async Task<BaseResponse> CreateAsync(SectionViewModel item, string createdBy)
        {
            var section = new Section
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                Fees = item.Fees,
                ProgramId = item.ProgramId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.Sections.Add(section);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Section created successfuly" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var secIndb = await _db.Sections.FindAsync(id);
            if (secIndb == null)
                return new BaseResponse() { Status = false, Message = "Section Not Found!" };
            _db.Sections.Remove(secIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "section Deleted Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public Task<IEnumerable<SectionViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SectionViewModel?> GetByIdAsync(Guid id)
        {
            return await _db.Sections.Select(x => new SectionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Fees = x.Fees,
                ProgramId = x.ProgramId,
                Program = new ProgramViewModel
                {
                    Id = x.Program.Id,
                    Name = x.Program.Name,
                    Description = x.Program.Description
                }
            }).SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedList<SectionViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.Sections.Select(x => new SectionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Fees = x.Fees,
                ProgramId = x.ProgramId,
                Program = new ProgramViewModel
                {
                    Id = x.Program.Id,
                    Name = x.Program.Name,
                    Description = x.Program.Description,
                }
            });
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize).
                Take(filter.PageSize).ToListAsync();
            return PaginatedList<SectionViewModel>.Create(items, count,filter);
        }

        public async Task<BaseResponse> UpdateAsync(SectionViewModel item, string updatedby)
        {
            var secIndb = await _db.Sections.FindAsync(item.Id);
            if(secIndb == null)
                return new BaseResponse() { Status = false, Message = "section not Found" };
            secIndb.Name=item.Name;
            secIndb.Fees = item.Fees;
            secIndb.ProgramId=item.ProgramId;
            _db.Sections.Update(secIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "secton Update Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had serve error!" };
        }
    }
}
