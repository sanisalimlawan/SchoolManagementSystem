using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Flutterwave.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ClassRepo : IClassRepo
    {
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        public ClassRepo(SchoolDbContext db,UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
            //FlutterwaveApi
        }
        public async Task<BaseResponse> CreateAsync(ClassViewModel item, string createdBy)
        {
            var validate = await _db.classes.SingleOrDefaultAsync(x => x.Name == item.Name);
            if(validate != null)
            {
                return new BaseResponse() { Status = false, Message = "Class with same Exist" };
            }
            var classes = new Class
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                sectionId = item.sectionId,
                TecherId = item.TeacherId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.classes.Add(classes);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Class created successfuly" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var clIndb = await _db.classes.FindAsync(id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "Class Not Found!" };
            _db.classes.Remove(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Class Deleted Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public Task<IEnumerable<ClassViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ClassViewModel?> GetByIdAsync(Guid id)
        {
            var classvm = await _db.classes.Include(s =>s.studentprograms).Include(x=>x.section).
                ThenInclude(p =>p.Program).Include(x => x.employee).ThenInclude(x=>x.UserId).Select(x => new ClassViewModel
            {
                Id = x.Id,
                Name = x.Name,
                sectionId =x.sectionId,
                section = new SectionViewModel
                {
                    Id = x.section.Id,
                    Name = x.section.Name,
                    Fees = x.section.Fees,
                    ProgramId = x.section.ProgramId,
                    Program = new ProgramViewModel
                    {
                        Id = x.section.Program.Id,
                        Name = x.section.Program.Name,
                        Description = x.section.Program.Description
                    }
                },
                    TotalSubject = x.Subjects.Count,
                TeacherId = x.TecherId,
                Teacher =  new EmployeeViewModel
                {
                    Id = x.employee.Id,
                    UserId = x.employee.UserId,
                } 
                }).SingleOrDefaultAsync(x => x.Id == id);
            if (classvm != null)
            {
                var user = await _userManager.FindByIdAsync(classvm.Teacher.UserId.ToString());
                if(user != null)
                {
                    classvm.Teacher.FirstName = user.FirstName;
                    classvm.Teacher.LastName = user.LastName;
                    classvm.Teacher.OthersName = user.OthersName;
                    classvm.Teacher.Email = user.Email;
                    classvm.Teacher.PhoneNumber = user?.PhoneNumber;
                    classvm.Teacher.UserName = user.UserName;
                }
            }

            return classvm;
        }

        public async Task<PaginatedList<ClassViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.classes.Include(x=>x.section).ThenInclude(x => x.Program).Include(x =>x.employee).Select(x => new ClassViewModel
            {
                Id = x.Id,
                Name = x.Name,
                sectionId = x.sectionId,
                section = new SectionViewModel
                {
                    Id = x.section.Id,
                    Name = x.section.Name,
                    Fees = x.section.Fees,
                    ProgramId = x.section.ProgramId,
                    Program = new ProgramViewModel
                    {
                        Id = x.section.Program.Id,
                        Name = x.section.Program.Name,
                        Description = x.section.Program.Description
                    }
                },
                TeacherId = x.TecherId,
                TotalSubject = x.Subjects.Count
            });
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize).
                Take(filter.PageSize).ToListAsync();

            foreach(var item in items)
            {
                if(item.TeacherId != Guid.Empty)
                {
                    var employee = await _db.employees.FirstOrDefaultAsync(x => x.Id == item.TeacherId);
                    if(employee != null)
                    {
                        var user = await _userManager.FindByIdAsync(employee.UserId.ToString());
                        if(user != null)
                        {
                            item.Teacher = new EmployeeViewModel
                            {
                                FirstName = user.FirstName,
                                LastName = user.LastName,
                                Email = user.Email,
                                PhoneNumber = user.PhoneNumber,
                                UserName = user.UserName
                            };
                        }
                    }
                }
            }
            return PaginatedList<ClassViewModel>.Create(items, count, filter);
        }

        public async Task<BaseResponse> UpdateAsync(ClassViewModel item, string updatedby)
        {
            var clIndb = await _db.classes.FindAsync(item.Id);
            if (clIndb == null)
                return new BaseResponse() { Status = false, Message = "Class not Found" };
            clIndb.Name = item.Name;
            clIndb.sectionId = item.sectionId;
            clIndb.TecherId = item.TeacherId;
            _db.classes.Update(clIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Class Update Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had serve error!" };

        }
    }
}
