using System;
using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class StudentRequirementViewModel : DateTimeViewModel
    {
        [Required]
        public int ProfileId { get; set; }

    }
}
