using GyanDyan.ViewModels;
using System.Threading.Tasks;

namespace GyanDyan.Services.Interfaces
{
    public interface IUserService
    {
        Task VolunteerRegister(VolunteerRegisterViewModel volunteerRegisterView);
        Task StudentRegister(StudentRegisterViewModel studentRegisterView);
        Task<TokenViewModel> VolunteerLogin(LoginViewModel userLogin);
        Task<TokenViewModel> StudentLogin(LoginViewModel userLogin);
    }
}
