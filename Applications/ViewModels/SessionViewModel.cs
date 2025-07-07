using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class SessionViewModel
    {
        public Guid Id { get; set; }
        [Required] 
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
    }
}
