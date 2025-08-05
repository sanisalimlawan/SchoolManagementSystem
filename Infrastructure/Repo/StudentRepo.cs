using Application.Constant;
using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class StudentRepo : IStudentRepo
    {
        private readonly IWebHostEnvironment _web;
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Role> _roleManager;
        //private readonly Flutterwave _flutterwaveSettings;
        public StudentRepo(IWebHostEnvironment web, SchoolDbContext db, UserManager<Persona> userManager, RoleManager<Role> roleManager)
        {
            _web = web;
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public async Task<BaseResponse> CreateAsync(StudentViewModel model, IFormFile file)
        {
            using var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var existingParent = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == model.Parent.PhoneNumber || x.Email == model.Parent.Email);
                if (existingParent != null)
                {
                    return new BaseResponse() { Status = false, Message = "Parent With Same PhoneNumber or Email exist" };
                }
                Persona user = new Persona
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    OthersName = model.OthersName,
                    Email = model.Email,
                    CreatedAt = DateTime.UtcNow,
                    Password = "Aa1234567",
                    UserName = model.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var parentaccount = new Persona
                {
                    Id = Guid.NewGuid(),
                    FirstName = model.Parent.FirstName,
                    LastName = model.Parent.LastName,
                    OthersName = model.Parent.OthersName,
                    Email = model.Parent.Email,
                    CreatedAt = DateTime.UtcNow,
                    Password = "Aa1234567",
                    UserName = model.Parent.PhoneNumber,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                var studentUserResult = await _userManager.CreateAsync(user, "Aa1234567");
                if (!studentUserResult.Succeeded)
                {
                    return new BaseResponse
                    {
                        Status = false,
                        Message = string.Join(", ", studentUserResult.Errors.Select(e => e.Description))
                    };
                }
                await _userManager.AddToRoleAsync(user, RoleConstant.Student);

                var parent = new Parent
                {
                    Id = Guid.NewGuid(),
                    Address = model.Parent.Address,
                    DOB = model.Parent.DOB,
                    Occupation = model.Parent.Occupation,
                    Religion = model.Parent.Religion,
                    UserId = parentaccount.Id,
                    Nationality = model.Parent.Nationality,
                    NIN = model.Parent.NIN,
                    RelationShip = model.Parent.RelationShip,
                    Gender = model.Parent.Gender,
                    ProfilePicture = model.Parent.ProfilePicture,
                    CreatedAt = DateTime.UtcNow,
                };

                if (model.Parent != null && model.ParentId == null)
                {
                    var response = await _userManager.CreateAsync(parentaccount, "Aa1234567");
                    if (!response.Succeeded)
                    {
                        return new BaseResponse
                        {
                            Status = false,
                            Message = string.Join(", ", response.Errors.Select(e => e.Description))
                        };
                    }
                    await _userManager.AddToRoleAsync(parentaccount, RoleConstant.Parent);
                }


                var student = new Student
                {
                    Id = model.Id,
                    UserId = user.Id,
                    Address = model.Address,
                    DOB = model.DOB,
                    BloodGroup = model.BloodGroup,
                    Gender = model.Gender,
                    Genotype = model.Genotype,
                    LocalGovnmentId = model.LocalGovnmentId,
                    Allergies = model.Allergies != null ? string.Join(',', model.Allergies) : null,
                    MedicalConditions = model.MedicalConditions != null ? string.Join(',', model.MedicalConditions) : null,

                    Height = model.Height,
                    Religion = model.Religion,
                    Weight = model.Weight,
                    RegNumber = await GenerateAdmissionNumber(),
                    ParentId = model.ParentId ?? parent.Id, // Use existing parent or newly created one

                };

                student.StudentPrograms = model.StudentPrograms.Select(x => new StudentProgram
                {
                    ProgramId = x.ProgramId,
                    ClassId = x.ClassId,
                    StudentId = student.Id
                }).ToList();
                if (model.Scholarship != null)
                {
                    student.Scholarship = new Scholarship
                    {
                        Id = Guid.NewGuid(),
                        StudentId = student.Id,
                        Type = model.Scholarship.Type,
                        Amount = model.Scholarship.Amount,
                        Percentage = model.Scholarship.Percentage,
                        Description = model.Scholarship.Description,
                        StartDate = model.Scholarship.StartDate,
                        EndDate = model.Scholarship.EndDate,
                        IsDeleted = false,
                        CreatedAt = DateTime.UtcNow
                    };
                }

                await _db.students.AddAsync(student); // Add the student
                await _db.parents.AddAsync(parent);   // Only if created

                var saveResult = await _db.TrySaveChangesAsync(); // or SaveChangesAsync()

                if (saveResult)
                {
                    await transaction.CommitAsync();
                    return new BaseResponse { Status = true, Message = "Student created successfully" };
                }
                await transaction.RollbackAsync();
                return new BaseResponse { Status = false, Message = "Something went wrong saving to the database" };

            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                //_logger.LogError(ex, "Failed to save student and parent");
                return new BaseResponse { Status = false, Message = $"Database error: {ex.InnerException?.Message ?? ex.Message}" };
            }


        }
        private async Task<string> GenerateAdmissionNumber()
        {
            var count = await _db.students.CountAsync() + 1;
            string studentId = count.ToString("D6"); // Pad with leading zeros to make it 6 digits
            string name = $"STU{studentId}"; // Prefix with "STU"
            string year = DateTime.Now.Year.ToString().Substring(2, 2); // Get last two digits of the current year
            string admissionNumber = $"DYB{name}-{year}"; // Combine the student ID and year
            return admissionNumber;
        }

        public Task<BaseResponse> CreateAsync(StudentViewModel item, string createdBy)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StudentViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<StudentViewModel?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<StudentViewModel> GetByUserId(Guid UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedList<StudentViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var student = _db.students
                .Include(x => x.LocalGovnment)
                .Include(x => x.StudentPrograms).ThenInclude(sp => sp.Program)
                .Include(x => x.StudentPrograms).ThenInclude(sp => sp.Class)
                .Select(x => new StudentViewModel
                {
                    Id = x.Id,
                    RegNumber = x.RegNumber,
                    Gender = x.Gender,
                    Genotype = x.Genotype,
                    BloodGroup = x.BloodGroup,
                    DOB = x.DOB,
                    Address = x.Address,
                    //Allergies = x.Allergies != null ? x.Allergies.Split(',').ToList() : new List<string>(),
                    Height = x.Height,
                    Weight = x.Weight,
                    Scholarship = x.Scholarship != null ? new ScholarshipViewModel
                    {
                        Id = x.Scholarship.Id,
                        Amount = x.Scholarship.Amount,
                        Percentage = x.Scholarship.Percentage,
                        Type = x.Scholarship.Type,
                        Description = x.Scholarship.Description,
                        StartDate = x.Scholarship.StartDate,
                        EndDate = x.Scholarship.EndDate
                    } : null,
                    Religion = x.Religion,
                    //MedicalConditions = x.MedicalConditions != null ? x.MedicalConditions.Split(',').ToList() : new List<string>(),
                    LocalGovnmentId = x.LocalGovnmentId,
                    LocalGovnment = new LocalGovernmentViewModel
                    {
                        Id = x.LocalGovnment.Id,
                        Name = x.LocalGovnment.Name
                    },
                    StudentPrograms = x.StudentPrograms.Select(sp => new StudentProgramViewModel
                    {
                        ProgramId = sp.ProgramId,
                        ClassId = sp.ClassId,
                        Program = new ProgramViewModel
                        {
                            Id = sp.Program.Id,
                            Name = sp.Program.Name
                        },
                        Class = new ClassViewModel
                        {
                            Id = sp.Class.Id,
                            Name = sp.Class.Name
                        }
                    }).ToList(),
                    Nationality = x.Nationality,
                    UserId = x.UserId,
                    ParentId = x.ParentId,
                    Parent = x.Parent != null ? new ParentViewModel
                    {
                        Id = x.Parent.Id,
                        Address = x.Parent.Address,
                        UserId = x.Parent.UserId,
                        NIN = x.Parent.NIN,
                        Nationality = x.Parent.Nationality,
                        DOB = x.Parent.DOB,
                        Occupation = x.Parent.Occupation,
                        Religion = x.Parent.Religion,
                        ProfilePicture = x.ProfilePicture
                    } : null,
                });
            var count = await student.CountAsync();
            var items = await student
                .Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();
            foreach (var item in items)
            {
                var user = await _userManager.FindByIdAsync(item.UserId.ToString());
                if (user != null)
                {
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.OthersName = user.OthersName;
                    item.Email = user?.Email;
                    item.PhoneNumber = user.PhoneNumber;
                    item.UserName = user.UserName;    // Assuming UserName is the phone number
                }

                if (item.ParentId != Guid.Empty)
                {
                    var parentEntity = await _db.parents.FirstOrDefaultAsync(p => p.Id == item.ParentId);
                    var parentUser = await _userManager.FindByIdAsync(parentEntity?.UserId.ToString());
                    if (parentEntity != null && parentUser != null)
                    {
                        item.Parent = new ParentViewModel
                        {
                            Id = parentEntity.Id,
                            Address = parentEntity.Address,
                            UserId = parentEntity.UserId,
                            NIN = parentEntity.NIN,
                            Nationality = parentEntity.Nationality,
                            DOB = parentEntity.DOB,
                            Occupation = parentEntity.Occupation,
                            Religion = parentEntity.Religion,
                            ProfilePicture = parentEntity.ProfilePicture,
                            FirstName = parentUser.FirstName,
                            LastName = parentUser.LastName,
                            OthersName = parentUser.OthersName,
                            PhoneNumber = parentUser.PhoneNumber,
                            Email = parentUser.Email,
                        };
                    }
                }

                // Split raw strings
                //item.Allergies = !string.IsNullOrEmpty(item.AllergiesRaw)
                //    ? item.AllergiesRaw.Split(',').ToList()
                //    : new List<string>();

                //item.MedicalConditions = !string.IsNullOrEmpty(item.MedicalConditionsRaw)
                //    ? item.MedicalConditionsRaw.Split(',').ToList()
                //    : new List<string>();
                return PaginatedList<StudentViewModel>.Create(items, count, filter);
            }
        }

        public Task<PaginatedList<StudentViewModel>> GetTeachersAsync(FilterOptions filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateAsync(StudentViewModel model, IFormFile file)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> UpdateAsync(StudentViewModel item, string updatedby)
        {
            throw new NotImplementedException();
        }
    }
}
