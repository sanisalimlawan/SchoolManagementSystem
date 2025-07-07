using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Term : Base
    {
        public string Name {  get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public Guid sessionId { get; set; }
        [ForeignKey(nameof(sessionId))]
        public Session session { get; set; }
    }
}
