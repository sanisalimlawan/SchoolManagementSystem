using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Class : Base
    {
        public string Name { get; set; }
        public Guid sectionId { get; set; }
        [ForeignKey(nameof(sectionId))]
        public Section section { get; set; }
        public Guid TecherId { get; set; }
        [ForeignKey(nameof(TecherId))]
        public Employee employee { get; set; }
        public ICollection<Subject> Subjects { get; set; } = new List<Subject>();
        public ICollection<Student> students { get; set; } = new List<Student>();
    }
}
