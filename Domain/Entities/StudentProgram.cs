using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentProgram  : Base
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid ProgramId { get; set; }
        public Program Program { get; set; }
        public Guid ClassId { get; set; }
        public Class Class { get; set; }
        public bool IsActive { get; set; } = true; // to track active enrollment

        public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    }
}
