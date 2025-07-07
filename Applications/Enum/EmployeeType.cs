using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enum
{
    public enum EmployeeType
    {
        Teaching,
        [Display(Name = "Non Teaching Staff")]
        NonTeaching
    }
}
