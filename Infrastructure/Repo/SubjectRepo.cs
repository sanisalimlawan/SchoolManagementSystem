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
    public class SubjectRepo : ISubjectRepo
    {
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        public SubjectRepo(SchoolDbContext db, UserManager<Persona> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public async Task<BaseResponse> CreateAsync(SubjectViewModel item, string createdBy)
        {
            var subject = new Subject
            {
                Id = Guid.NewGuid(),
                Name = item.Name,
                SubjectTeacherId = item.SubjectTeacherId,
                ClassId = item.ClassId,
                TotalCAMark = item.TotalCAMark,
                TotalExamMark = item.TotalExamMark,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };
            _db.Subjects.Add(subject);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Subject created successfuly" };
            return new BaseResponse() { Status = false, Message = "Subject we had a server error!" };

        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            var sbjIndb = await _db.Subjects.FindAsync(id);
            if (sbjIndb == null)
                return new BaseResponse() { Status = false, Message = "Subject Not Found!" };
            _db.Subjects.Remove(sbjIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Subject Deleted Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had a server error!" };

        }

        public Task<IEnumerable<SubjectViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<SubjectViewModel?> GetByIdAsync(Guid id)
        {
            var Subjectvm = await _db.Subjects.Include(s => s.Class).Include(x => x.SubjectTeacher)
                .Select(x => new SubjectViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    ClassId = x.ClassId,
                    Classes = new ClassViewModel
                    {
                        Id = x.Class.Id,
                        Name = x.Class.Name,
                    },
                    TotalExamMark = x.TotalExamMark,
                    TotalCAMark = x.TotalCAMark,
                    SubjectTeacherId = x.SubjectTeacherId,
                    SubjectTeacher = new EmployeeViewModel
                    {
                        Id = x.SubjectTeacher.Id,
                        UserId = x.SubjectTeacher.UserId,
                    }
                }).SingleOrDefaultAsync(x => x.Id == id);
            if (Subjectvm != null)
            {
                var user = await _userManager.FindByIdAsync(Subjectvm.SubjectTeacher.UserId.ToString());
                if (user != null)
                {
                    Subjectvm.SubjectTeacher.FirstName = user.FirstName;
                    Subjectvm.SubjectTeacher.LastName = user.LastName;
                    Subjectvm.SubjectTeacher.OthersName = user.OthersName;
                    Subjectvm.SubjectTeacher.Email = user?.Email;
                    Subjectvm.SubjectTeacher.PhoneNumber = user?.PhoneNumber;
                    Subjectvm.SubjectTeacher.UserName = user.UserName;
                }
            }

            return Subjectvm;
        }

        public async Task<PaginatedList<SubjectViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.Subjects.Include(x => x.Class).Include(x => x.SubjectTeacher).Select(x => new SubjectViewModel
            {
                Id = x.Id,
                Name = x.Name,
                TotalCAMark = x.TotalCAMark,
                TotalExamMark = x.TotalExamMark,
                Classes = new ClassViewModel
                {
                    Id = x.Class.Id,
                    Name = x.Class.Name,
                },
                SubjectTeacherId = x.SubjectTeacherId,
                SubjectTeacher = new EmployeeViewModel
                {
                    Id = x.SubjectTeacher.Id,
                    UserId = x.SubjectTeacher.UserId,
                }
            });
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize).
                Take(filter.PageSize).ToListAsync();

            foreach (var item in items)
            {
                if (item.SubjectTeacherId != Guid.Empty)
                {
                    var employee = await _db.employees.FirstOrDefaultAsync(x => x.Id == item.SubjectTeacherId);
                    if (employee != null)
                    {
                        var user = await _userManager.FindByIdAsync(employee.UserId.ToString());
                        if (user != null)
                        {
                            item.SubjectTeacher = new EmployeeViewModel
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
            return PaginatedList<SubjectViewModel>.Create(items, count, filter);
        }

        public async Task<BaseResponse> UpdateAsync(SubjectViewModel item, string updatedby)
        {
            var sbjIndb = await _db.Subjects.FindAsync(item.Id);
            if (sbjIndb == null)
                return new BaseResponse() { Status = false, Message = "Subject not Found" };
            sbjIndb.Name = item.Name;
            sbjIndb.ClassId = item.ClassId;
            sbjIndb.TotalCAMark = item.TotalCAMark;
            sbjIndb.TotalExamMark = item.TotalExamMark;
            sbjIndb.SubjectTeacherId = item.SubjectTeacherId;
            _db.Subjects.Update(sbjIndb);
            var result = await _db.TrySaveChangesAsync();
            if (result)
                return new BaseResponse() { Status = true, Message = "Subject Update Successfully" };
            return new BaseResponse() { Status = false, Message = "sorry we had serve error!" };

        }
    }
}
