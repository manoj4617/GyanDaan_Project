using GyanDyan.ViewModels;
using System.Threading.Tasks;

namespace GyanDyan.Services.Interfaces
{
    public interface IRequirement
    {
        Task AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel);
        Task AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel);
    }
}
