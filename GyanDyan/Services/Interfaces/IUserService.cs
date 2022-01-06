using GyanDyan.ViewModels;
using System.Threading.Tasks;

namespace GyanDyan.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> VolunteerRegister(VolunteerRegisterViewModel volunteerRegisterView);
        Task<string> StudentRegister(StudentRegisterViewModel studentRegisterView);
        Task<TokenViewModel> VolunteerLogin(LoginViewModel userLogin);
        Task<TokenViewModel> StudentLogin(LoginViewModel userLogin);
    }
}
