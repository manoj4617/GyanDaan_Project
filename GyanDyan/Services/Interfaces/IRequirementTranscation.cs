using GyanDyan.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Services
{
    public interface IRequirementTranscation
    {
        Task<string> NotificationRequirementByStudent(int volunteerId,int volunteerRequirementId, int studentId);
        Task<string> AcceptedByVolunteer(int volunteerID, int requirementId, int studentId);
        string RejectedByVolunteer(int volunteerID,int requirementId, int studentId);
        Task<List<SendNotificationDetials>> GetAllNotificationsForVolunteer(int volunteerId);
        Task<IEnumerable<VolunteerRequirement>> AcceptStudentRequirement(int studentRequirementId, int volunteerId);
        Task<string> InviteThisStudentReq(int studentReqId, int volunteerReqId);
        Task<List<VolunteerInbox>> GetReqListForStudents(int studentId);
        Task<List<StudentInbox>> GetReqListForVolunteer(int volunteerId);
        Task<List<StudentInbox>> GetInvitationsForStudent(int studentId);
        Task<string> AcceptInvitation(int inviteId);
        string RejectedInvitation(int inviteId);
        Task<List<Group>> GetStudnetInGroupClass(int studentId);
        Task<List<OneToOne>> GetStudentInOneToOneClass(int studentId);
    }
}
