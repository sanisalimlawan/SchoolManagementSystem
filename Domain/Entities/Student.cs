using Application.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : Base
    {
        public Guid UserId { get; set; }
        public string RegNumber { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string? MedicalConditions { get; set; }  // Multiple select
        public string? Allergies { get; set; }
        public string? BloodGroup { get; set; }
        public string? Genotype { get; set; }
        public int? Height { get; set; } // in cm
        public int? Weight { get; set; } // in kg
        public string Nationality {get; set; }
        public Guid LocalGovnmentId { get; set; }
        [ForeignKey(nameof(LocalGovnmentId))]
        public LocalGovnment LocalGovnment { get; set; }

        public Scholarship? Scholarship { get; set; }
        // Many-to-Many with Program
        public ICollection<StudentProgram> StudentPrograms { get; set; } = new List<StudentProgram>();
        public Guid ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Parent Parent { get; set; }
    }

}
