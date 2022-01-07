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
               .Select(v => new { v.VolunteerRequirement, v.StudentId, v.VolunteerId })
               .ToListAsync();

            foreach (var i in getNotification)
            {
                if (i.VolunteerRequirement.TypeOfClass == TypeOfClass.OneToOne)
                {
                    var addInOneToOne = new OneToOne()
                    {
                        StudentId = i.StudentId,
                        VolunteerRequirementId = i.VolunteerRequirement.Id
                    };
                    await _context.OneToOneClass.AddAsync(addInOneToOne);
                    RemoveNotification(volunteerID, requirementId, studentId);
                }
                if (i.VolunteerRequirement.TypeOfClass == TypeOfClass.Group)
                {
                    var addInGroup = new Group()
                    {
                        VolunteerRequirementId = i.VolunteerRequirement.Id,
                        StudentId = i.StudentId
                    };
                    await _context.GroupsClass.AddAsync(addInGroup);
                    RemoveNotification(volunteerID, requirementId, studentId);
                }
            }

            return "Accepted";
        }

        public string RejectedByVolunteer(int volunteerID, int requirementId, int studentId)
        {
            RemoveNotification(volunteerID, requirementId, studentId);
            return "Rejected";
        }

        public async Task<List<SendNotificationDetials>> GetAllNotificationsForVolunteer(int volunteerId)
        {
            return await _context.VolunteerInboxes
                 .Where(id => id.VolunteerId == volunteerId)
                 .Select(v => new SendNotificationDetials()
                 {
                     StudentProfile = _context.StudentProfiles.Where(id => id.Id == v.StudentId)
                         .FirstOrDefault(),
                     Volunteer = v.VolunteerRequirement,
                 })
                 .ToListAsync();
        }

        public async Task<string> AcceptStudentRequirement(int studentRequirementId, int volunteerId)
        {
            var getStudentTypeOfClass = _context.StudentRequirements
                .Where(rid => rid.Id == studentRequirementId)
                .FirstOrDefault();

            var volunteerRequirement = await _context.VolunteerRequirements
                .Where(id => id.VolunteerProfileId == volunteerId
                    && id.AreaOfSpecialization == getStudentTypeOfClass.Topic)
                    .FirstOrDefaultAsync();
            
            //If the volunteer trying to admit the student does not have any class with the same topic as the student requirement
            //and same type of class the she/he cannot admit the studnet to the class
            if(volunteerRequirement == null)
            {
                return $"You cannot accept this student in any of your classes since you don't have any classes" +
                    $" for {getStudentTypeOfClass.Topic}";
            }

            if(volunteerRequirement.TypeOfClass != getStudentTypeOfClass.TypeOfClass)
            {
                return $"Your existing type of class doesn't match with the student's type of class";
            }

            if (getStudentTypeOfClass.TypeOfClass == TypeOfClass.OneToOne)
            {
                var addNewStudent = new OneToOne()
                {
                    StudentRequirementId = studentRequirementId,
                    VolunteerProfileId = volunteerId
                };
                getStudentTypeOfClass.AcceptedByVolunteer = true;
                await _context.OneToOneClass.AddAsync(addNewStudent);
                SaveChangesToDB();

                return $"Added student to {volunteerRequirement.AreaOfSpecialization} class";
            }
            if(getStudentTypeOfClass.TypeOfClass == TypeOfClass.Group)
            {
                var addNewStudent = new Group()
                {
                    VolunteerRequirementId = volunteerRequirement.Id,
                    StudentId = getStudentTypeOfClass.StudentProfileId,
                    StudentRequirementId = studentRequirementId
                };
                getStudentTypeOfClass.AcceptedByVolunteer = true;
                await _context.GroupsClass.AddAsync(addNewStudent);
                SaveChangesToDB();

                return $"Added student to {volunteerRequirement.AreaOfSpecialization} class";
            }

            return "Not added";
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

        private void SaveChangesToDB()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}

