using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Section : Base
    {
        public string Name { get; set; }
        public Guid ProgramId { get; set; }
        [ForeignKey(nameof(ProgramId))]
        public Program Program { get; set; }
        public double Fees { get; set; }
    }
}
