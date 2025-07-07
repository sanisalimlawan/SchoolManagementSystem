using Application.Constant;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.SeedData
{
    public static class UserExtentions
    {
        public static async Task SeedRoleAsync(this RoleManager<Role> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(RoleConstant.SuperAdmin))
            {
                _=await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.SuperAdmin });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.Principal))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.Principal });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.HeadMaster))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.HeadMaster });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.FormMaster))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.FormMaster });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.Teacher))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.Teacher });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.Student))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.Student });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.Parent))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.Parent});
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.Accountant))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.Accountant });
            }
            if (!await roleManager.RoleExistsAsync(RoleConstant.ExamOfficer))
            {
                _ = await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.ExamOfficer });
            }
        }
        public static async Task SeedUserAsync(this UserManager<Persona> userManager)
        {
            if (await userManager.FindByNameAsync("SuperAdmin") is not null)
                return;
            var user = new Persona
            {
                Id = Guid.NewGuid(),
                FirstName = "Super",
                LastName = "Admin",
                PhoneNumber = Environment.GetEnvironmentVariable("SUPER_ADMIN_PHONE"),
                Email = Environment.GetEnvironmentVariable("SUPER_ADMIN_EMAIL"),
                UserName = "SuperAdmin",
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
            };

            var result = await userManager.CreateAsync(user, Environment.GetEnvironmentVariable("SUPER_ADMIN_PASSWORD")??"");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, RoleConstant.SuperAdmin);
            }

        }
    }
}
