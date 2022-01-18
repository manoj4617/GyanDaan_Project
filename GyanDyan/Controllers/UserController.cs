using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using GyanDyan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

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

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpPut("update-student-profile/{studentId}")]
        public async Task<IActionResult> UpdateStudentProfile([FromRoute]int studentId,[FromBody] ProfileUpdateViewModel studentProfile)
        {
            return Ok(await _userService.UpdateStudentProfile(studentId, studentProfile));
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpPut("update-volunteer-profile/{volunteerId}")]
        public async Task<IActionResult> UpdateVolunteerProfile([FromRoute]int volunteerId, [FromBody] ProfileUpdateViewModel volunteerProfile)
        {
            return Ok(await _userService.UpdateVolunteerProfile(volunteerId, volunteerProfile));
        }


        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-student-profile/{studentId}")]
        public async Task<IActionResult> GetStudentProfile([FromRoute] int studentId)
        {
            return Ok(await _userService.GetStudentDetails(studentId));
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("get-volunteer-profile/{volunteerId}")]
        public async Task<IActionResult> GetVolunteerProfile([FromRoute] int volunteerId)
        {
            return Ok(await _userService.GetVolunteerDetails(volunteerId));
        }
    }
}
