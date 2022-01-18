using GyanDyan.ViewModels;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> VolunteerRegister(VolunteerRegisterViewModel volunteerRegisterView);
        Task<string> StudentRegister(StudentRegisterViewModel studentRegisterView);
        Task<TokenViewModel> VolunteerLogin(LoginViewModel userLogin);
        Task<TokenViewModel> StudentLogin(LoginViewModel userLogin);
        Task<string> UpdateStudentProfile(int studentId, ProfileUpdateViewModel studentProfile);
        Task<string> UpdateVolunteerProfile(int volunteerId, ProfileUpdateViewModel volunteerProfile);
        Task<StudentProfile> GetStudentDetails(int studentId);
        Task<VolunteerProfile> GetVolunteerDetails(int volunteerId);
    }
}
