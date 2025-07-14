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

        public Gender Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public MarialStatus MarialStatus { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }

        public Guid LocalGovnmentId { get; set; }
        [ForeignKey(nameof(LocalGovnmentId))]
        public LocalGovnment LocalGovnment { get; set; }

        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }

        // Many-to-Many with Program
        public ICollection<StudentProgram> StudentPrograms { get; set; } = new List<StudentProgram>();
    }

}
