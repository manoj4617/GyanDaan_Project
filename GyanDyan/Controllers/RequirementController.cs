using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using GyanDyan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GyanDyan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementController : ControllerBase
    {
        private readonly IRequirement _studentRequirements;

        public RequirementController(IRequirement studentRequirements)
        {
            _studentRequirements = studentRequirements;
        }


        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpPost("new-student-requirement")]
        public async Task<IActionResult> AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel)
        {
            await _studentRequirements.AddNewStudentRequirement(requirementViewModel);
            return Ok("Your new requirement was added!!!");
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpPost("new-volunteer-requirement")]
        public async Task<IActionResult> AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel)
        {
            await _studentRequirements.AddNewVolunteerRequirement(requirementViewModel);
            return Ok("Your new requirement was added!!!");
        }
    }
}
