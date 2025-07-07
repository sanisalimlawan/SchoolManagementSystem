using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Enum
{
    public enum EmployeeStatus
    {
        Active,
        Suspended,
        Resigned,
        Terminated,
        Retired,
        [Display(Name = "On Leave")]
        OnLeave,
        Probation
    }
}
