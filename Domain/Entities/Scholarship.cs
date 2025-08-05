using Application.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Scholarship : Base
    {
        public Guid StudentId { get; set; }
        public Student Student { get; set; }

        public ScholarshipType Type { get; set; } // Enum: Amount or Percentage

        public decimal? Amount { get; set; }       // Only if Type == Amount
        public decimal? Percentage { get; set; }     // Only if Type == Percentage

        public string? Description { get; set; }   // Optional notes (e.g. scholarship name)

        public DateTime StartDate { get; set; } = DateTime.UtcNow;  // Date the student Start Scholarship
        public DateTime? EndDate { get; set; }  // Date the student End Scholarship
    }
}
