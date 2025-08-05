using Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Parent : Base
    {
        public Guid UserId { get; set; }
        public Gender Gender { get; set; }  
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string RelationShip { get; set; }
        public string Nationality { get; set; }
        public string NIN { get; set; }
        public ICollection<Student> students { get; set; } = new List<Student>();
    }
}
