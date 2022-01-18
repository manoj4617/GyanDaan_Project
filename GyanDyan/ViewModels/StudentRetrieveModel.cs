using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class StudentRetrieveModel :DateTimeViewModel
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int StudentProfileId { get; set; }

        [Required]
        public DateTime PostedOnDate { get; set; }

        [Required]
        public String Email { get; set; }

        [Required]
        public String Password { get; set; }


        }
}
