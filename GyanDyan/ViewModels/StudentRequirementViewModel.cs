using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class StudentRequirementViewModel : DateTimeViewModel
    {
        [Required]
        public int StudentProfileId { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public DateTime TimeOfStart { get; set; }
    }
}
