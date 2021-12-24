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
        private readonly IStudentRequirement _studentRequirements;

        public RequirementController(IStudentRequirement studentRequirements)
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
    }
}
