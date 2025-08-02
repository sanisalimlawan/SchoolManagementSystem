using Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ParentViewModel
    {
        public Guid UserId { get; set; }
        public Gender Gender { get; set; }  
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public ICollection<StudentViewModel> students { get; set; } = new List<StudentViewModel>();
    }
}
