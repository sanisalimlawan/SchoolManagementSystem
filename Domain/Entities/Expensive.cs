using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Expensive : Base
    {
        public decimal Amount { get; set; }
        public string description { get; set; }
        public DateTime Date { get; set; }
    }
}
