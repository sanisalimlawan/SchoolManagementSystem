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
        public DbSet<StudentProgram> StudentPrograms { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<LocalGovnment> localGovnments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Subject>()
            .HasOne(s => s.SubjectTeacher)
            .WithMany()
            .HasForeignKey(s => s.SubjectTeacherId)
            .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Subject>()
           .HasOne(s => s.Class).WithMany(c=> c.Subjects). HasForeignKey(x=>x.ClassId)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Class>()
                .HasMany(x => x.students).WithOne(x => x.Class).HasForeignKey(x => x.ClassId);
            modelBuilder.Entity<Student>()
              .HasOne(x => x.LocalGovnment).WithMany().HasForeignKey(x => x.LocalGovnmentId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>()
                .HasOne(x => x.Class).WithMany(x => x.students).HasForeignKey(x => x.ClassId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StudentProgram>()
        .HasKey(sp => new { sp.StudentId, sp.ProgramId });

            modelBuilder.Entity<StudentProgram>()
                .HasOne(sp => sp.Student)
                .WithMany(s => s.StudentPrograms)
                .HasForeignKey(sp => sp.StudentId)
    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StudentProgram>()
                .HasOne(sp => sp.Program)
                .WithMany(p => p.StudentPrograms)
                .HasForeignKey(sp => sp.ProgramId)
    .OnDelete(DeleteBehavior.Restrict);

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
