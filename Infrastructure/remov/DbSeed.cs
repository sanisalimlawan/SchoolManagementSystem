using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.SeedData
{
    public class DbSeed : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;
        public DbSeed(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await using var scope = _serviceProvider.CreateAsyncScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Persona>>();
            var dbContext = scope.ServiceProvider.GetRequiredService<SchoolDbContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbSeed>>();

            try
            {
                logger.LogInformation("Applying Migration");
                await dbContext.Database.MigrateAsync(cancellationToken: cancellationToken);
                logger.LogInformation("MIgration Successfully");

                logger.LogInformation("Seeding Roles");
                await roleManager.SeedRoleAsync();
                logger.LogInformation("Roles Seeded Successfully");

                logger.LogInformation("Seeding User");
                await userManager.SeedUserAsync();
                logger.LogInformation("User Seeded Successfully");

                logger.LogInformation("Seeding States");
                await dbContext.SeedState();
                logger.LogInformation("States Seeded Successfully");

                logger.LogInformation("Seeding LocalGovernments");
                await dbContext.SeedLocalGovernment();
                logger.LogInformation("LocalGovernments Seeded Succcessfully");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
