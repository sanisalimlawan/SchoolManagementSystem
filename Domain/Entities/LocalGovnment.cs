using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class LocalGovnment: Base
    {
        public string Name { get; set; }
        public Guid StateId { get; set; }
        public State State { get; set; }
    }
}
