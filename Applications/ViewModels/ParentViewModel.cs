using Application.Constant;
using Application.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels
{
    public class ParentViewModel
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
        public Gender Gender { get; set; }  
        public DateOnly DOB { get; set; }
        public string? ProfilePicture { get; set; }
        public string Religion { get; set; }
        public string Address { get; set; }
        public string Occupation { get; set; }
        public string RelationShip { get; set; }
        public string Nationality { get; set; }
        public string NIN { get; set; }
        public ICollection<StudentViewModel> students { get; set; } = new List<StudentViewModel>();
    }
}
