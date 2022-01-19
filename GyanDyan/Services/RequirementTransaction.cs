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
                        StudentId = i.StudentId,
                        VolunteerProfileId = i.VolunteerId
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
                 .Include(i => i.StudentProfile)
                 .Select(v => new SendNotificationDetials()
                 {
                     StudentProfile = _context.StudentProfiles.Where(id => id.Id == v.StudentId)
                         .FirstOrDefault(),
                     Volunteer = v.VolunteerRequirement,
                 })
                 .ToListAsync();
        }

        public async Task<IEnumerable<VolunteerRequirement>> AcceptStudentRequirement(int studentRequirementId, int volunteerId)
        {
            var getStudentTypeOfClass = _context.StudentRequirements
                .Where(rid => rid.Id == studentRequirementId)
                .FirstOrDefault();

            var volunteerRequirement = await _context.VolunteerRequirements
                .Where(id => id.VolunteerProfileId == volunteerId
                        && id.Subject == getStudentTypeOfClass.Subject 
                        && id.TypeOfClass == getStudentTypeOfClass.TypeOfClass
                        || id.Topic == getStudentTypeOfClass.Topic)
                    .ToListAsync();

            var studentInbox = await _context.StudentInboxes
                .Where(id => id.StudentRequirementId == studentRequirementId && id.VolunteerId == volunteerId)
                .Select(id => id.VolunteerRequirement)
                .ToListAsync();
            IEnumerable<VolunteerRequirement> newvolunteerRequirement = volunteerRequirement.Except(studentInbox);
            if (volunteerRequirement == null)
            {
                return null;
            }

            return newvolunteerRequirement;
        }

        public async Task<string> InviteThisStudentReq(int studentReqId, int volunteerReqId)
        {
            var getVolunteer = await _context.VolunteerRequirements
                .Where(i => i.Id == volunteerReqId)
                .FirstOrDefaultAsync();

            var getStudent = await _context.StudentRequirements
                .Where(i => i.Id == studentReqId)
                .FirstOrDefaultAsync();

            var inviteStudnet = new StudentInbox()
            {
                StudentRequirementId = studentReqId,
                StudentId = getStudent.StudentProfileId,
                VolunteerId = getVolunteer.VolunteerProfileId,
                VolunteerRequirementId = volunteerReqId
            };

            await _context.StudentInboxes.AddAsync(inviteStudnet);
            SaveChangesToDB();

            return $"Your invitation was sent successfully!!!";
        }
        public async Task<List<VolunteerInbox>> GetReqListForStudents(int studentId)
        {
            return await _context.VolunteerInboxes
                .Where(id => id.StudentId == studentId)
                .Include(i => i.VolunteerRequirement)
                .ToListAsync();
        }

        public async Task<List<StudentInbox>> GetReqListForVolunteer(int volunteerId)
        {
            return await _context.StudentInboxes
                .Where(id => id.VolunteerId == volunteerId)
                .Include(i => i.StudentRequirement)
                .ToListAsync();
        }

        public async Task<List<StudentInbox>> GetInvitationsForStudent(int studentId)
        {
            return await _context.StudentInboxes
                .Include(i => i.StudentRequirement)
                .Include(i => i.VolunteerRequirement)
                .ThenInclude(i => i.VolunteerProfile)
                .Where(id => id.StudentId == studentId)
                .ToListAsync();
        }

        public async Task<string> AcceptInvitation(int inviteId)
        {
            var getInvite = await _context.StudentInboxes
                .Include(i => i.StudentRequirement)
                .Include(i => i.VolunteerRequirement)
                .ThenInclude(i => i.VolunteerProfile)
                .Where(id => id.Id == inviteId)
                .FirstOrDefaultAsync();

            var getStudentReq = _context.StudentRequirements
               .Where(rid => rid.Id == getInvite.StudentRequirementId)
               .FirstOrDefault();

            if (getInvite.StudentRequirement.TypeOfClass == TypeOfClass.Group)
            {
                var addInGroupClass = new Group()
                {
                    VolunteerRequirementId = getInvite.VolunteerRequirementId,
                    StudentRequirementId = getInvite.StudentRequirementId,
                    StudentId = getInvite.StudentId,
                    VolunteerProfileId = getInvite.VolunteerRequirement.VolunteerProfileId
                };
                await _context.GroupsClass.AddAsync(addInGroupClass);
                getStudentReq.AcceptedByVolunteer = true;
                RemoveInvitation(inviteId);
            }
            if(getInvite.StudentRequirement.TypeOfClass == TypeOfClass.OneToOne)
            {
                var addIn1to1 = new OneToOne()
                {
                    StudentRequirementId = getInvite.StudentRequirement.Id,
                    VolunteerProfileId = getInvite.VolunteerRequirement.VolunteerProfileId,
                };
                await _context.OneToOneClass.AddAsync(addIn1to1);
                getStudentReq.AcceptedByVolunteer = true;
                RemoveInvitation(inviteId);
            }
            SaveChangesToDB();
            return $"You have accepted the invitation from {getInvite.VolunteerRequirement.VolunteerProfile.FirstName} for {getStudentReq.Subject}";
        }

        public string RejectedInvitation(int inviteId)
        {
            RemoveInvitation(inviteId);
            SaveChangesToDB();
            return "Rejected";
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

        private  void RemoveInvitation(int inviteId)
        {
            var rm =  _context.StudentInboxes.Where(id => id.Id == inviteId).FirstOrDefault();
            _context.StudentInboxes.Remove(rm);
        }
        private void SaveChangesToDB()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}

