using GyanDyan.Services;
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
    public class RequirementTranscationController : ControllerBase
    {
        private readonly IRequirementTranscation _requirementTranscation;

        public RequirementTranscationController(IRequirementTranscation requirementTranscation)
        {
            _requirementTranscation = requirementTranscation;
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("send-request/{volunteerId}/{requirementId}/{studentId}")]
        public async Task<string> SendRequestToVolunteer([FromRoute]int volunteerId,[FromRoute] int requirementId , [FromRoute] int studentId)
        {
            return await _requirementTranscation.NotificationRequirementByStudent(volunteerId,requirementId, studentId);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("accept-request/{volunteerId}/{requirementId}/{studentId}")]
        public async Task<string> AcceptRequestByVolunteer([FromRoute] int volunteerId,[FromRoute] int requirementId, [FromRoute] int studentId)
        {
            return await _requirementTranscation.AcceptedByVolunteer(volunteerId,requirementId, studentId);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("reject-request/{volunteerId}/{requirementId}/{studentId}")]
        public async Task<string> RejectRequestByVolunteer([FromRoute] int volunteerId, [FromRoute] int requirementId, [FromRoute] int studentId)
        {
            return  _requirementTranscation.RejectedByVolunteer(volunteerId,requirementId, studentId);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("get-notifications/{reqid}")]
        public async Task<List<SendNotificationDetials>> GetAllTheNotifications([FromRoute] int reqid)
        {
            return await _requirementTranscation.GetAllNotificationsForVolunteer(reqid);
        }
    }
}
