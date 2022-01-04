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
        Task<string> AcceptStudentRequirement(int studentRequirementId, int volunteerId);
    }
}
