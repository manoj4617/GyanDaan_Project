using GyanDyan.ViewModels;
using System.Threading.Tasks;

namespace GyanDyan.Services.Interfaces
{
    public interface IStudentRequirement
    {
        Task AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel);
    }
}
