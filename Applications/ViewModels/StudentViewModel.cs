using Application.Constant;
using Application.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class StudentViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string? OthersName { get; set; }
        public string FullName { get => $"{FirstName} {LastName} {OthersName}"; }
        public string? Password { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(StringConstants.PHONE_NUMBER_REGEX, ErrorMessage = "Invalid PhoneNumber")]
        public string PhoneNumber { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string RegNumber { get; set; }
        public Gender Gender { get; set; }
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public List<string>? MedicalConditions { get; set; }  // Multiple select
        public List<string>? Allergies { get; set; }
        public string? BloodGroup { get; set; }
        public string? Genotype { get; set; }
        public int? Height { get; set; } // in cm
        public int? Weight { get; set; } // in kg
        public Guid LocalGovnmentId { get; set; }
        public LocalGovernmentViewModel LocalGovnment { get; set; }

        public Guid ClassId { get; set; }
        public ClassViewModel Class { get; set; }

        // Many-to-Many with Program
        public List<StudentProgramViewModel> StudentPrograms { get; set; } = new List<StudentProgramViewModel> ();
        public Guid ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ParentViewModel Parent
        {
            get; set;
        }
    }
}
