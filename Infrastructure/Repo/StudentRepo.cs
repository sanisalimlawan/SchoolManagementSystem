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
            //var UserIndb = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == model.UserName);
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

            var parent = new Parent
            {
                Id = Guid.NewGuid(),
                Address = model.Parent.Address,
                DOB = model.Parent.DOB,
                Occupation = model.Parent.Occupation,
                Religion = model.Parent.Religion
            };

            var student = new Student
            {
                Id = model.Id,
                UserId = user.Id,
                Address = model.Address,
                DOB = model.DOB,
                Allergies = model.Allergies,
                BloodGroup = model.BloodGroup,
                Gender = model.Gender,
                Genotype = model.Genotype,
                LocalGovnmentId = model.LocalGovnmentId,
                MedicalConditions = model.MedicalConditions,
                Height = model.Height,
                Religion = model.Religion,
                Weight = model.Weight,

            };
           
            student.StudentPrograms = model.StudentPrograms.Select(x => new StudentProgram
            {
                ProgramId = x.ProgramId,
                ClassId = x.ClassId,
                StudentId = student.Id
            }).ToList();

            throw new NotImplementedException();
        }
        private async Task<string> GenerateAdmissionNumber()
        {
            string name = "sani";
            return name;
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

        public Task<PaginatedList<StudentViewModel>> GetPaginatedList(FilterOptions filter)
        {
            throw new NotImplementedException();
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
