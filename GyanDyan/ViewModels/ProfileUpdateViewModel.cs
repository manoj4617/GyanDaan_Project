using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class ProfileUpdateViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(20), MinLength(3)]
        public string FirstName { get; set; }

        [MaxLength(20)]
        public string LastName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Enter your Email Id")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required"), MaxLength(10), MinLength(10)]
        public string MobileNumber { get; set; }

        public string Street { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string City { get; set; }

        [Required, MinLength(3), MaxLength(15)]
        public string State { get; set; }

        [Required, Range(100000, 999999)]
        public long Pin { get; set; }

        [Required(ErrorMessage = "Please select your Education Qualification")]
        public string EducationQualification { get; set; }
    }
}
