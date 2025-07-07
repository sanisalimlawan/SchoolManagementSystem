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
        public Guid ClassId { get; set; }
        [ForeignKey(nameof(ClassId))]
        public Class Classes { get; set; }
    }
}
