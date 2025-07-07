using Application.Constant;
using Application.ViewModels;
using Domain.Entities;
using Infrastructure.Services.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Implementation
{
    public class LocationServices : ILocationsSevices
    {
        private readonly SchoolDbContext _db;
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<Persona> _userManager;
        public LocationServices(SchoolDbContext db, RoleManager<Role> roleManager, UserManager<Persona> userManager)
        {
            _db = db;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<IEnumerable<RoleViewModel>> GetAllRoles()
        {
            return await _roleManager.Roles.Where(x=> x.Name != RoleConstant.SuperAdmin && x.Name != RoleConstant.Parent && x.Name != RoleConstant.Student)
                .Select(x => new RoleViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();
        }

        public async Task<IEnumerable<SessionViewModel>> GetAllSessions()
        {
            var sessions = await _db.Sessions.Select(x => new SessionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                
            }).ToListAsync();

            return sessions;
        }

        public async Task<IEnumerable<StateViewModel>> GetStatesAsync()
        {
            return await _db.states.Select(x => new StateViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<LocalGovernmentViewModel>> GetLocalGovernmentsAsync(Guid stateId)
        {
            return await _db.localGovnments.Where(x => x.StateId == stateId)
                .Select(x => new LocalGovernmentViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    StateId = x.StateId

                }).OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<ProgramViewModel>> GetAllProgramAsync()
        {
            var program = await _db.Programs.Select(x => new ProgramViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            }).OrderBy(x => x.Name).ToListAsync();
            return program;
        }

        public async Task<IEnumerable<SectionViewModel>> GetAllSectionAsync()
        {
            var sections = await _db.Sections.Include(c=>c.Program).Select(x => new SectionViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Fees = x.Fees,
                ProgramId = x.ProgramId
            }).OrderBy(x => x.Name).ToListAsync();
            return sections;
        }

        public async Task<IEnumerable<EmployeeViewModel>> GetAllFormMastersAsync()
        {
            //  Get all Teaching employees
            var teachingEmployees = await _db.employees
                .Where(e => e.EmployeeType == Application.Enum.EmployeeType.Teaching)
                .Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    UserId = e.UserId
                }).ToListAsync();

            var result = new List<EmployeeViewModel>();

            // Step 2: Enrich with Identity info and filter only FormMasters
            foreach (var emp in teachingEmployees)
            {
                var user = await _userManager.FindByIdAsync(emp.UserId.ToString());
                if (user != null)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles.Contains(RoleConstant.FormMaster)) // Only include those with FormMaster role
                    {
                        emp.FirstName = user.FirstName;
                        emp.LastName = user.LastName;
                        emp.Role = string.Join(",", roles);
                        result.Add(emp); // Add to result only if FormMaster
                    }
                }
            }

            return result;
        }

    }
}
