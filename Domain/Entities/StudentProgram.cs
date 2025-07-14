using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentProgram : Base
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public Guid ProgramId { get; set; }
        public Program Program { get; set; }
    }
}
