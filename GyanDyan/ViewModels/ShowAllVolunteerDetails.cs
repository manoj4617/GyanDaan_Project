using static GyanDyan.Models.Domain;

namespace GyanDyan.ViewModels
{
    public class ShowAllVolunteerDetails 
    {
        /*public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public EducationQualification EducationQualification { get; set; }*/
        public int VolunteerReqId { get; set; }
        public VolunteerRequirement VolunteerRequirement { get; set; }
    }
}
