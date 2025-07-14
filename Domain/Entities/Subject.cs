using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Subject : Base
    {
        public string Name { get; set; }
        public int TotalCAMark { get; set; }
        public int TotalExamMark { get; set; }
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Class { get; set; }
        public Guid SubjectTeacherId { get; set; }
        [ForeignKey(nameof(SubjectTeacherId))]
        public Employee SubjectTeacher { get; set; }
    }
}
