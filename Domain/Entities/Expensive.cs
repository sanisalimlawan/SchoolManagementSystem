using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Expensive : Base
    {
        public decimal Amount { get; set; }                 // amount spent, e.g., 1500.00
        public string description { get; set; }             // e.g., "Office Supplies", "Software License"
        public DateTime DateSpent { get; set; }             // When the expense was incurred
        public string Vendor { get; set; }                 // e.g., "Tech World Ltd"
        public string Category { get; set; }               // e.g., "Stationery"
        public string PaymentMethod { get; set; }          // e.g., "Bank Transfer"
        public string? ReferenceNumber { get; set; }       // Optional payment ref
        public Guid? ApprovedByUserId { get; set; }        // Who approved
        //public Persona? ApprovedByUser { get; set; }       // Navigation
    }
}
