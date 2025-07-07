using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Application.ViewModels
{
    public class TermViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public bool IsCurrent
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);
                return StartDate <= today && EndDate >= today;
            }
        }
        public Guid SessionId { get; set; }
        public SessionViewModel session { get; set; }
    }
}
