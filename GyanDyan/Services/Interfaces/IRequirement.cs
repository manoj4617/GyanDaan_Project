using GyanDyan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Services.Interfaces
{
    public interface IRequirement
    {
        Task<string> AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel);
        Task<string> AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel);
        Task<IEnumerable<StudentRequirement>> GetStudentRequirements(int studentId);
        Task<IEnumerable<VolunteerRequirement>> GetVolunteerRequirements(int volunteerId);
        Task<IEnumerable<VolunteerRequirement>> ShowAllVolunteerDetailsForStudent(int studentId);
        Task<IEnumerable<StudentRequirement>> ShowAllStudentRequirment(int volunteerId);
    }
}
