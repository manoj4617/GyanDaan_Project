using System.ComponentModel.DataAnnotations;

namespace GyanDyan.ViewModels
{
    public class VolunteerRequirementViewModel : DateTimeViewModel
    {
        [Required]
        public int ProfileId { get; set; }
    }
}
