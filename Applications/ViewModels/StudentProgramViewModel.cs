
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class StudentProgramViewModel 
    {
        public Guid Id { get; set; }
        public Guid StudentId { get; set; }
        public StudentViewModel Student { get; set; }

        public Guid ProgramId { get; set; }
        public ProgramViewModel Program { get; set; }
        public Guid ClassId { get; set; }
        public ClassViewModel Class { get; set; }
    }
}
