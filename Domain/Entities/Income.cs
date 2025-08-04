using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Income : Base
    {
        public decimal Amount { get; set; }         // amoint e.g., 1500.00
        public string description { get; set; }      // e.g., "Tuition Fee", "Donation"
        public DateTime DateRecieve {  get; set; }     // Date recieve
        public string Source { get; set; }              // e.g., "John Doe", "Ministry"
        public string Category { get; set; }            // e.g., "Donation", "Tuition"
        public string PaymentMethod { get; set; }       // e.g., "Cash", "Bank Transfer"
        public string? ReferenceNumber { get; set; }    // e.g., Flutterwave ref
        public Guid? ReceivedByUserId { get; set; }     // FK to admin who entered it
        //public Persona? ReceivedByUser { get; set; }
    }
}
