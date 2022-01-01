using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class VolunteerRegisterViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(20), MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Choose your gender")]
        public string Gender { get; set; }

        [Required, EmailAddress(ErrorMessage = "Enter your Email Id")]
        public string Email { get; set; }

        [Required, RegularExpression(pattern: "^.*(?=.{8,})(?=.*[a-zA-Z])(?=.*\\d)(?=.*[!#$@%&? \"]).*$", ErrorMessage = "Minimum 8 characters, must contain a digit, a special character, and a capital letter")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone number is required"), MaxLength(10), MinLength(10)]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        public string Street { get; set; }

        [Required,MinLength(3),MaxLength(15)]
        public string City { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string State { get; set; }

        [Required, Range(100000, 999999)]
        public long Pin { get; set; }

        [Required(ErrorMessage = "Please select your Education Qualification")]
        public string EducationQualification { get; set; }
    }
}
