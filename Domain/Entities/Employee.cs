using Application.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Employee :Base
    {
        public Guid UserId { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public EmployeeStatus Status { get; set; } 
        public Gender Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string? ProfilePic { get; set; }
        public MarialStatus MarialStatus { get; set; }
        public string Religion { get; set; }
        public string Address {  get; set; }
        public int? Salary { get; set; }
        public Guid LocalGovnmentId { get; set; }
        [ForeignKey(nameof(LocalGovnmentId))]
        public LocalGovnment localGovnment { get; set; }

        
    }
}
