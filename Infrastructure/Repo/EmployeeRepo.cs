using Application.Enum;
using Application.IRepo;
using Application.ViewModels;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly IWebHostEnvironment _web;
        private readonly SchoolDbContext _db;
        private readonly UserManager<Persona> _userManager;
        private readonly RoleManager<Role> _roleManager;
        public EmployeeRepo(IWebHostEnvironment web,SchoolDbContext db, UserManager<Persona> userManager,RoleManager<Role> roleManager)
        {
            _web=web;
            _db=db;
            _userManager=userManager;
            _roleManager=roleManager;
        }
        public async Task<BaseResponse> CreateAsync(EmployeeViewModel model, IFormFile file)
        {
            var UserIndb = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == model.Email || x.UserName == model.UserName || x.PhoneNumber == model.PhoneNumber);
            if (UserIndb != null)
                return new BaseResponse() { Status = false, Message = "Employee with same UserName or Email or PhoneNumber Exist" };
            var user = new Persona
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                OthersName = model.FirstName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Password = "Aa1234567",
                UserName = model.PhoneNumber,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };
            user.UserName = model.PhoneNumber;
            var imageurl = file != null && file.Length > 0 ? await SaveImageAsync(file) : null ;

            var employee = new Employee
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                MarialStatus = model.MarialStatus,
                DOB = model.DOB,
                EmployeeType = model.EmployeeType,
                LocalGovnmentId = model.LocalGovnmentId,
                Address = model.Address,
                Gender = model.Gender,
                Religion = model.Religion,
                Salary = model.Salary,
                ProfilePic = imageurl,
                Status = EmployeeStatus.Active
            };

            _db.employees.Add(employee);
             var status = await _db.TrySaveChangesAsync();

            await _userManager.SetUserNameAsync(user, user.PhoneNumber);
            if(!string.IsNullOrWhiteSpace(user.Email))
                await _userManager.SetEmailAsync(user, user.Email);
            var result = await _userManager.CreateAsync(user, user.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, model.Role);
            }
            else
            {
                return new BaseResponse() { Status = false, Message = "hello whats goin on!" };
            }
            if (status)
                return new BaseResponse() { Status = true, Message = "Employee Created successfully" };
            return new BaseResponse() { Status = false, Message = "Unable to create Employee check back!" };
        }

        private async Task<string> SaveImageAsync(IFormFile file)
        {
            //string wwrooPath = _web.WebRootPath;
            //    var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            //    var EmployeeImagePath = Path.Combine(wwrooPath, @"Images/Employees", fileName);
            //    if(!Directory.Exists(EmployeeImagePath))
            //        Directory.CreateDirectory(EmployeeImagePath);

            //    using(var filestream = new FileStream(EmployeeImagePath, FileMode.Create))
            //    {
            //        await file.CopyToAsync(filestream);
            //    }

            //return EmployeeImagePath;
            string fileName = $"{Guid.NewGuid}_{file.FileName}";
            string imagePath = Path.Combine(_web.WebRootPath, "Employees", fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(imagePath) ?? throw new InvalidOperationException("Directory not found"));

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return $"/Employees/{fileName}";
        }
        public Task<BaseResponse> CreateAsync(EmployeeViewModel item, string createdBy)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> DeleteAsync(Guid id)
        {
            using(var transaction = await _db.Database.BeginTransactionAsync())
            {
                try
                {
                    //fecth Employee
                    var Employee = await _db.employees.SingleOrDefaultAsync(x => x.Id == id);
                    if (Employee == null)
                    {
                        return new BaseResponse() { Status = false, Message = "Employee not Found!" };
                    }
                    //fecth user in db
                    var userIndb = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == Employee.UserId);
                    if (userIndb == null)
                    {
                        return new BaseResponse() { Status = false, Message = "User not found!" };
                    }


                    // ✅ Delete image from wwwroot
                    if (!string.IsNullOrWhiteSpace(Employee.ProfilePic))
                    {
                        string relativePath = Employee.ProfilePic.TrimStart('/', '\\').Replace("/", Path.DirectorySeparatorChar.ToString());
                        var imagePath = Path.Combine(_web.WebRootPath, relativePath);

                        if (System.IO.File.Exists(imagePath))
                        {
                            System.IO.File.Delete(imagePath);
                        }
                        else
                        {
                            Console.WriteLine("❌ File not found at: " + imagePath);
                        }
                    }

                    // remove the two tables
                    _db.employees.Remove(Employee);
                    var deleteUserresult = await _userManager.DeleteAsync(userIndb);
                    if (!deleteUserresult.Succeeded)
                    {
                        await transaction.RollbackAsync();
                        return new BaseResponse() { Status = false, Message = "Failed to Delete User" };
                    }

                    var status = await _db.TrySaveChangesAsync();
                    if (!status)
                    {
                        await transaction.RollbackAsync();
                        return new BaseResponse() { Status = false, Message = "Sorry we Had a Server Error" };
                    }
                    await transaction.CommitAsync();
                    return new BaseResponse() { Status = status, Message = "Employee Deleted Successfully" };

                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    return new BaseResponse() { Status = false, Message = "An error occurred while deleting the employee!" };
                }
            }

        }

        public Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeViewModel?> GetByIdAsync(Guid id)
        {
            var EmployeeIndb = await _db.employees.Include(x => x.localGovnment).ThenInclude(x=>x.State).SingleOrDefaultAsync(x => x.Id == id);
            if (EmployeeIndb == null)
                return null;
            var userIndb = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == EmployeeIndb.UserId);
            if(userIndb == null)
                return null;
            var roleIndb = await _userManager.GetRolesAsync(userIndb);
            var employee = new EmployeeViewModel
            {
                Id = EmployeeIndb.Id,
                FirstName = userIndb.FirstName,
                LastName = userIndb.LastName,
                OthersName = userIndb.OthersName,
                DOB = EmployeeIndb.DOB,
                Gender = EmployeeIndb.Gender,
                MarialStatus = EmployeeIndb.MarialStatus,
                Address = EmployeeIndb.Address,
                Email = userIndb?.Email,
                PhoneNumber = userIndb?.PhoneNumber,
                LocalGovnmentId = EmployeeIndb.LocalGovnmentId,
                Religion = EmployeeIndb.Religion,
                Salary = EmployeeIndb.Salary,
                EmployeeType = EmployeeIndb.EmployeeType,
                Role = string.Join(',',roleIndb),
                Status = EmployeeIndb.Status,
                localGovnment = new LocalGovernmentViewModel
                {
                    Id = EmployeeIndb.localGovnment.Id,
                    Name = EmployeeIndb.localGovnment.Name,
                    state = new StateViewModel
                    {
                        Name = EmployeeIndb.localGovnment.State.Name,
                        Id = EmployeeIndb.localGovnment.StateId,

                    }
                },
                ProfilePic = EmployeeIndb.ProfilePic,
                UserName = userIndb.UserName,
            };
            return employee;
        }

        public async Task<PaginatedList<EmployeeViewModel>> GetPaginatedList(FilterOptions filter)
        {
            var data = _db.employees.Include(u => u.localGovnment).ThenInclude(x=>x.State).Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                Salary = x.Salary,
                Religion = x.Religion,
                Address = x.Address,
                ProfilePic = x.ProfilePic,
                DOB = x.DOB,
                Gender = x.Gender,
                MarialStatus = x.MarialStatus,
                EmployeeType = x.EmployeeType,
                LocalGovnmentId = x.LocalGovnmentId,
                UserId = x.UserId,
                Status = x.Status,
                localGovnment = new LocalGovernmentViewModel
                {
                    Id = x.localGovnment.Id,
                    Name = x.localGovnment.Name,
                    state = new StateViewModel
                    {
                        Id = x.localGovnment.StateId,
                        Name = x.localGovnment.State.Name
                    }
                },

            }).AsNoTracking();
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            foreach(var item in items)
            {
                var user = await _userManager.FindByIdAsync(item.UserId.ToString());

                if (user != null)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    item.Email = user.Email;
                    item.PhoneNumber = user.PhoneNumber;
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.OthersName = user.OthersName;
                    item.UserName = user.UserName;
                    item.Role = string.Join(',', role);
                }
            }

            return PaginatedList<EmployeeViewModel>.Create(items, count,filter);
        }

        public Task<BaseResponse> UpdateAsync(EmployeeViewModel item, string updatedby)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeViewModel> GetByUserId(Guid UserId)
        {

            var EmployeeIndb = await _db.employees.Include(x => x.localGovnment)
                .ThenInclude(x => x.State)
                .SingleOrDefaultAsync(x => x.UserId == UserId);
            var userIndb = await _userManager.Users.SingleAsync(x => x.Id == EmployeeIndb!.UserId);
            var roleIndb = _userManager.GetRolesAsync(userIndb);
            var employee = new EmployeeViewModel
            {
                Id = EmployeeIndb.Id,
                FirstName = userIndb.FirstName,
                LastName = userIndb.LastName,
                OthersName = userIndb.OthersName,
                DOB = EmployeeIndb.DOB,
                Gender = EmployeeIndb.Gender,
                MarialStatus = EmployeeIndb.MarialStatus,
                Address = EmployeeIndb.Address,
                Email = userIndb?.Email,
                Status = EmployeeIndb.Status,
                PhoneNumber = userIndb?.PhoneNumber,
                LocalGovnmentId = EmployeeIndb.LocalGovnmentId,
                Religion = EmployeeIndb.Religion,
                Salary = EmployeeIndb.Salary,
                EmployeeType = EmployeeIndb.EmployeeType,
                Role = roleIndb.ToString(),
                localGovnment = new LocalGovernmentViewModel
                {
                    Id = EmployeeIndb.localGovnment.Id,
                    Name = EmployeeIndb.localGovnment.Name,
                    state = new StateViewModel
                    {
                        Name = EmployeeIndb.localGovnment.State.Name,
                        Id = EmployeeIndb.localGovnment.StateId,

                    }
                },
                ProfilePic = EmployeeIndb.ProfilePic,
                UserName = userIndb.UserName,
            };
            return employee;
        }

        public async Task<BaseResponse> UpdateAsync(EmployeeViewModel model, IFormFile file)
        {
            var employeeindb = await _db.employees.SingleOrDefaultAsync(x => x.Id == model.Id);
            if (employeeindb == null)
                return new BaseResponse() { Status = false, Message = "Employee Not Found" };
            var UserIndb = await _userManager.Users.SingleOrDefaultAsync(x => x.Id == employeeindb.UserId);
            if (UserIndb == null)
                return new BaseResponse() { Status = false, Message = "User Not Found" };
            UserIndb.FirstName = model.FirstName;
            UserIndb.LastName = model.LastName;
            UserIndb.OthersName = model.OthersName;
            UserIndb.Email = model.Email;
            UserIndb.PhoneNumber = model.PhoneNumber;
            await _userManager.UpdateAsync(UserIndb);

            employeeindb.DOB = model.DOB;
            employeeindb.MarialStatus = model.MarialStatus;
            employeeindb.Salary = model.Salary;
            employeeindb.Gender = model.Gender;
            employeeindb.Address = model.Address;
            employeeindb.LocalGovnmentId = model.LocalGovnmentId;
            employeeindb.EmployeeType = model.EmployeeType;
            employeeindb.Religion = model.Religion;
            employeeindb.Status = model.Status;
            employeeindb.LastModefiedBy = "Admin";
            if(file != null && file.Length > 0)
            {
                // Delete old image
                if (!string.IsNullOrEmpty(employeeindb.ProfilePic))
                {
                    string oldRelativePath = employeeindb.ProfilePic.TrimStart('/', '\\').Replace("/", Path.DirectorySeparatorChar.ToString());
                    var oldImagePath = Path.Combine(_web.WebRootPath, oldRelativePath);
                    if (System.IO.File.Exists(oldImagePath))
                        System.IO.File.Delete(oldImagePath);

                    var newImage = await SaveImageAsync(file);
                    employeeindb.ProfilePic = newImage;
                }
            }

            _db.employees.Update(employeeindb);
            var Status = await _db.TrySaveChangesAsync();
            if (Status)
                return new BaseResponse() { Status = true, Message = "Employee Updated Successfully" };
            return new BaseResponse() { Status = false, Message = "Unable to Update Employee try Again Later" };

        }

        public async Task<PaginatedList<EmployeeViewModel>> GetTeachersAsync(FilterOptions filter)
        {
            var data = _db.employees.Where(x=>x.EmployeeType == EmployeeType.Teaching).Include(u => u.localGovnment).ThenInclude(x => x.State).Select(x => new EmployeeViewModel
            {
                Id = x.Id,
                Salary = x.Salary,
                Religion = x.Religion,
                Address = x.Address,
                ProfilePic = x.ProfilePic,
                DOB = x.DOB,
                Gender = x.Gender,
                MarialStatus = x.MarialStatus,
                EmployeeType = x.EmployeeType,
                LocalGovnmentId = x.LocalGovnmentId,
                UserId = x.UserId,
                Status = x.Status,
                localGovnment = new LocalGovernmentViewModel
                {
                    Id = x.localGovnment.Id,
                    Name = x.localGovnment.Name,
                    state = new StateViewModel
                    {
                        Id = x.localGovnment.StateId,
                        Name = x.localGovnment.State.Name
                    }
                },

            }).AsNoTracking();
            var count = await data.CountAsync();
            var items = await data.Skip((filter.PageIndex - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
            foreach (var item in items)
            {
                var user = await _userManager.FindByIdAsync(item.UserId.ToString());

                if (user != null)
                {
                    var role = await _userManager.GetRolesAsync(user);
                    item.Email = user.Email;
                    item.PhoneNumber = user.PhoneNumber;
                    item.FirstName = user.FirstName;
                    item.LastName = user.LastName;
                    item.OthersName = user.OthersName;
                    item.UserName = user.UserName;
                    item.Role = string.Join(',', role);
                }
            }

            return PaginatedList<EmployeeViewModel>.Create(items, count, filter);
        }
    }
}
