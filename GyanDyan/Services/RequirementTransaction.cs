using GyanDyan.DataAccess;
using GyanDyan.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Services
{
    public class RequirementTransaction : IRequirementTranscation
    {
        private readonly Context _context;

        public RequirementTransaction(Context context)
        {
            _context = context;
        }

        //Student shows interest in volunteer requirement 
        public async Task<string> NotificationRequirementByStudent(int volunteerId, int volunteerRequirementId, int studentId)
        {
            var checkIfNotificationExists = await _context.VolunteerInboxes
                .Where(id => id.VolunteerRequirementId == volunteerRequirementId
                && id.StudentId == studentId).FirstOrDefaultAsync();

            if (checkIfNotificationExists != null)
            {
                return $"Your Request is still pending";
            }

            var addtoInbox = new VolunteerInbox()
            {
                VolunteerRequirementId = volunteerRequirementId,
                StudentId = studentId,
                VolunteerId = volunteerId
            };

            await _context.VolunteerInboxes.AddAsync(addtoInbox);

            _context.SaveChanges();
            return $"Your Request has been sent";
        }

        public async Task<string> AcceptedByVolunteer(int volunteerID, int requirementId, int studentId)
        {
            var getNotification = await _context.VolunteerInboxes
               .Where(id => id.VolunteerId == volunteerID 
                   && id.VolunteerRequirementId == requirementId
                   && id.StudentId == studentId)
               .Select(v => new { v.VolunteerRequirement, v.StudentId, v.VolunteerId})
               .ToListAsync();

            foreach(var i in getNotification)
            {
                if(i.VolunteerRequirement.TypeOfClass == TypeOfClass.OneToOne)
                {
                    var addInOneToOne = new OneToOne()
                    {
                        StudentId = i.StudentId,
                        VolunteerRequirementId = i.VolunteerRequirement.Id
                    };
                    await _context.OneToOneClass.AddAsync(addInOneToOne);
                    RemoveNotification(volunteerID,requirementId, studentId);
                }
                if (i.VolunteerRequirement.TypeOfClass == TypeOfClass.Group)
                {
                    var addInGroup = new Group()
                    {
                        VolunteerRequirementId = i.VolunteerRequirement.Id,
                        StudentId = i.StudentId
                    };
                    await _context.GroupsClass.AddAsync(addInGroup);
                    RemoveNotification(volunteerID,requirementId, studentId);
                }
            }

            return "Accepted";
        }
            
        public string RejectedByVolunteer(int volunteerID,int requirementId, int studentId)
        {
            RemoveNotification(volunteerID,requirementId, studentId);
            return "Rejected";
        }

        public async Task<List<SendNotificationDetials>> GetAllNotificationsForVolunteer(int volunteerId)
        {
           return  await _context.VolunteerInboxes
                .Where(id => id.VolunteerId== volunteerId)
                .Select(v => new SendNotificationDetials()
                {
                    StudentProfile = _context.StudentProfiles.Where(id => id.Id == v.StudentId)
                        .FirstOrDefault(),
                    Volunteer = v.VolunteerRequirement,
                })
                .ToListAsync();
        }



        #region PRIVATE METHODS
        private void RemoveNotification(int volunteerID,int requirementId, int studentId)
        {
            var toRemove = _context.VolunteerInboxes.Where(id => id.VolunteerRequirementId == requirementId 
            && id.StudentId == studentId
            && id.VolunteerId == volunteerID).FirstOrDefault();
            _context.VolunteerInboxes.Remove(toRemove);
            _context.SaveChanges();
        }
        #endregion
    }
}

