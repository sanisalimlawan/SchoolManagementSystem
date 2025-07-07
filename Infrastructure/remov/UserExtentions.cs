using Application.Constant;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public static class UserExtentions
    {
        public static async Task SeedRoleAsync(this RoleManager<Role> roleManager)
        {
            if(!await roleManager.RoleExistsAsync(RoleConstant.SuperAdmin))
            {
                _=await roleManager.CreateAsync(new Role { Id = Guid.NewGuid(), Name = RoleConstant.SuperAdmin });
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
