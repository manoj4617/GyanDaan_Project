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

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("accept-student-request/{studentRequirementId}/{volunteerId}")]
        public async Task<IEnumerable<VolunteerRequirement>> AcceptStudentRequirement(int studentRequirementId, int volunteerId)
        {
            return await _requirementTranscation.AcceptStudentRequirement(studentRequirementId, volunteerId);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("send-inviteTo-student/{studentReqId}/{volunteerReqId}")]
        public async Task<string> InviteThisStudent([FromRoute] int studentReqId, [FromRoute] int volunteerReqId)
        {
            return await _requirementTranscation.InviteThisStudentReq(studentReqId, volunteerReqId);
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-invitesfor-student/{studentId}")]
        public async Task<List<StudentInbox>> GetStudentInboxesAsync([FromRoute]int studentId)
        {
            return await _requirementTranscation.GetInvitationsForStudent(studentId);
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-pending/{studentId}")]
        public async Task<List<VolunteerInbox>> GetVolunteerReqForStudent(int studentId)
        {
            return await _requirementTranscation.GetReqListForStudents(studentId);
        }

        [Authorize(Policy = StaticProvider.VolunteerPolicy)]
        [HttpGet("get-pending-for-volunteer/{volunteerId}")]
        public async Task<List<StudentInbox>> GetStudentReqForVolunteer(int volunteerId)
        {
            return await _requirementTranscation.GetReqListForVolunteer(volunteerId);
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("accept-invite/{inviteId}")]
        public async Task<string> AcceptInvitation([FromRoute]int inviteId)
        {
            return await _requirementTranscation.AcceptInvitation(inviteId);
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("reject-invite/{inviteId}")]
        public async Task<string> RejectInvitation([FromRoute] int inviteId)
        {
            return  _requirementTranscation.RejectedInvitation(inviteId);
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-in-group/{studentId}")]
        public async Task<IActionResult> GetStudentInGroup(int studentId)
        {
            return Ok(await _requirementTranscation.GetStudnetInGroupClass(studentId));
        }

        [Authorize(Policy = StaticProvider.StudentPolicy)]
        [HttpGet("get-in-oneToOne/{studentId}")]
        public async Task<IActionResult> GetStudentInOneToOne(int studentId)
        {
            return Ok(await _requirementTranscation.GetStudentInOneToOneClass(studentId));
        }
    }
}
