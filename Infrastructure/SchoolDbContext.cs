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
        public DbSet<Parent> parents { get; set; }
        public DbSet<StudentProgram> StudentPrograms { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<State> states { get; set; }
        public DbSet<LocalGovnment> localGovnments { get; set; }
        public DbSet<Income> incomes { get; set; }
        public DbSet<Expensive> expensives { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }



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
                .HasMany(x => x.studentprograms).WithOne(x => x.Class).HasForeignKey(x => x.ClassId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Student>()
              .HasOne(x => x.LocalGovnment).WithMany().HasForeignKey(x => x.LocalGovnmentId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Student>().HasMany(x => x.StudentPrograms).WithOne(x => x.Student).HasForeignKey(x => x.StudentId).OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Student>().HasMany(x => x.StudentPrograms).WithOne(x=>x.Class)
            
            modelBuilder.Entity<Student>().HasOne(x => x.Parent).WithMany(x => x.students).HasForeignKey(x => x.ParentId);
            modelBuilder.Entity<StudentProgram>()
        .HasKey(sp =>  sp.Id);

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
            modelBuilder.Entity<StudentProgram>()
    .HasOne(sp => sp.Class)
    .WithMany()
    .HasForeignKey(sp => sp.ClassId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Program>()
    .HasMany(p => p.StudentPrograms)
    .WithOne(sp => sp.Program)
    .HasForeignKey(sp => sp.ProgramId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Income>()
    .Property(i => i.Amount)
    .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Expensive>() // or Expense if typo
                .Property(e => e.Amount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Program>()
                .Property(p => p.ApplicationFee)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Scholarship>().HasOne(s => s.Student)
    .WithOne(st => st.Scholarship)
    .HasForeignKey<Scholarship>(s => s.StudentId);

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
