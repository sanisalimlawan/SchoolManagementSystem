using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Student : Base
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OthersName { get; set; }
        //public 
        public ICollection<Program> Programs { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
        public string ProfilePicture { get; set; }
    }
}
