using GyanDyan.Services.Interfaces;
using GyanDyan.Utils;
using GyanDyan.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequirementController : ControllerBase
    {
        private readonly IRequirement _requirements;

        public RequirementController(IRequirement studentRequirements)
        {
            _requirements = studentRequirements;
        }


        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpPost("new-student-requirement")]
        public async Task<IActionResult> AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel)
        { 
            return Ok(await _requirements.AddNewStudentRequirement(requirementViewModel));
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpPost("new-volunteer-requirement")]
        public async Task<IActionResult> AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel)
        {
            return Ok(await _requirements.AddNewVolunteerRequirement(requirementViewModel));
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-student-requirement/{id}")]
        public async Task<IEnumerable<StudentRequirement>> GetAllStudentRequirementsById(int id)
        {
            return await _requirements.GetStudentRequirements(id);
        }


        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("get-volunteer-requirement/{id}")]
        public async Task<IEnumerable<VolunteerRequirement>> GetAllVolunteerRequirementsById(int id)
        {
            return await _requirements.GetVolunteerRequirements(id);
        }


        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-all-student-requirement/{id}")]
        public async Task<IEnumerable<VolunteerRequirement>> GetAllTheRequirementsStudent(int id)
        {
            return await _requirements.ShowAllVolunteerDetailsForStudent(id); 
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("get-all-volunteer-requirement/{id}")]
        public async Task<IEnumerable<StudentRequirement>> GetAllTheRequirementsVolunteer(int id)
        {
            return await _requirements.ShowAllStudentRequirment(id);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpPut("update-volunteer-requirement/{volunteerReqId}")]
        public async Task<IActionResult> UpdateVolunteerRequirement([FromRoute]int volunteerReqId, [FromBody]VolunteerRequirementViewModel volunteerRequirementView)
        {
            return Ok(await _requirements.UpdateVolunteerRequirement(volunteerReqId, volunteerRequirementView));
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpPut("update-student-requirement/{studentReqId}")]
        public async Task<IActionResult> UpdateStudentRequirement([FromRoute] int studentReqId, [FromBody] StudentRequirementViewModel studentRequirementView)
        {
            return Ok(await _requirements.UpdateStudentRequirement(studentReqId, studentRequirementView));
        }
    }
}
