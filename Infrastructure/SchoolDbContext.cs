using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class SchoolDbContext : IdentityDbContext<Persona, Role,Guid>
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options):base(options)
        {
        }

        public DbSet<Program> Programs { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Term> Terms { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<LocalGovnment> localGovnments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configurations can be added here
        }

        public async Task<bool> TrySaveChangesAsync()
        {
            try
            {
                await SaveChangesAsync();
                return true;
            }catch(Exception ex)
            {
                Console.WriteLine("Error While Saving Changes",ex.Message);
                return false;
            }
        }
    }
}
