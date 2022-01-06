using GyanDyan.Services.Interfaces;
using GyanDyan.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GyanDyan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register-student")]
        public async Task<IActionResult> RegisterStudnet(StudentRegisterViewModel studentRegisterView)
        {
            return Ok(await _userService.StudentRegister(studentRegisterView));
        }

        [HttpPost("register-volunteer")]
        public async Task<IActionResult> RegisterVolunteer(VolunteerRegisterViewModel volunteerRegisterView)
        { 
            return Ok(await _userService.VolunteerRegister(volunteerRegisterView));
        }

        [HttpPost("student-login")]
        public async Task<IActionResult>StudentLogin(LoginViewModel loginViewModel)
        {
            return Ok(await _userService.StudentLogin(loginViewModel));
        }

        [HttpPost("volunteer-login")]
        public async Task<IActionResult> VolunteerLogin(LoginViewModel loginViewModel)
        {
            return Ok(await _userService.VolunteerLogin(loginViewModel));
        }
    }
}
