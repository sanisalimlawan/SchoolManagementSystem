using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SectionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Fees { get; set; }
        public Guid ProgramId { get; set; }
        public ProgramViewModel Program { get; set; }
    }
}
