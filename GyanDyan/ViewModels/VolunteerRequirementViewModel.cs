using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class VolunteerRequirementViewModel : DateTimeViewModel
    {
        [Required]
        public int VolunteerProfileId { get; set; }
        [Required]
        public string AreaOfspecialization { get; set; }
    }
}
