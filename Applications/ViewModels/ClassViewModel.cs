using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ClassViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid sectionId { get; set; }
        public Guid TeacherId { get; set; }
        public EmployeeViewModel Teacher { get; set; }
        public SectionViewModel section { get; set; }
        public int TotalStudent { get; set; }
        //public List<StudentViewModel> student = new List<StudentViewModel>();
    }
}
