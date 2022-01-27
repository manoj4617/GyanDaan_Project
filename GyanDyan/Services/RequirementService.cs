using GyanDyan.DataAccess;
using GyanDyan.Services.Interfaces;
using GyanDyan.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System;
using System.Threading.Tasks;
using static GyanDyan.Models.Domain;
using GyanDyan.Exceptions;
using System.Collections.Generic;

namespace GyanDyan.Services
{
    public class RequirementService : IRequirement
    {
        private readonly Context _studentContext;
        private readonly IConfiguration _configuration;

        public RequirementService(Context studentContext, IConfiguration configuration)
        {
            _studentContext = studentContext;
            _configuration = configuration;
        }

        public async Task<string> AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel)
        {
            //This query gets all the student requirement for the particular studnet 
            //which have same timings so that the requirements timing dont clash
            var clash = await CheckIfStudentDaysClash(requirementViewModel);
            if (clash != null)
            {
                return $"The selected days clash with the schedule of {clash.Subject} subject of topic {clash.Topic}";
            }

            var newStudentRequirement = new StudentRequirement()
            {
                StudentProfileId = requirementViewModel.ProfileId,
                PostedOnDate = GetDateTime().ToString("yyyy - MM - dd HH: mm:ss"),
                StartDay = (Days)Enum.Parse(typeof(Days),requirementViewModel.StartDay),
                EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay),
                StartTime = requirementViewModel.StartTime,
                EndTime = requirementViewModel.EndTime,
                Subject = requirementViewModel.Subject,
                Topic = requirementViewModel.Topic,
                TypeOfClass = (TypeOfClass)Enum.Parse(typeof(TypeOfClass),requirementViewModel.TypeOfClass)
            };

            await _studentContext.StudentRequirements.AddAsync(newStudentRequirement);

            SaveChangesToDB();

            return $"Your Requirement was added successfully!!!";
        }

        public async Task<string> AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel)
        {
            var clash = await CheckIfVolunteerDaysClash(requirementViewModel);
            if(clash != null)
            {
                return $"The selected days clash with the schedule of {clash.Subject} subject of topic {clash.Topic}";
            }
            var newVolunteerRequirement = new VolunteerRequirement()
            {
                VolunteerProfileId = requirementViewModel.ProfileId,
                PostedOnDate = GetDateTime().ToString("yyyy - MM - dd HH: mm:ss"),
                StartDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.StartDay),
                EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay),
                StartTime = requirementViewModel.StartTime,
                EndTime = requirementViewModel.EndTime,
                Subject = requirementViewModel.Subject,
                Topic = requirementViewModel.Topic,
                TypeOfClass = (TypeOfClass)Enum.Parse(typeof(TypeOfClass), requirementViewModel.TypeOfClass)
            };

            await _studentContext.VolunteerRequirements.AddAsync(newVolunteerRequirement);

            SaveChangesToDB();
            return $"Your Requirement was added successfully!!!";
        }

        //Getting Student Requirements
        public async Task<IEnumerable<StudentRequirement>> GetStudentRequirements(int studentId)
        {
            var requirements = await _studentContext.StudentRequirements.Where(id => id.StudentProfileId == studentId)
                .Include(i => i.OneToOne)
                .ThenInclude(i => i.VolunteerProfile)
                .Include(i => i.Group)
                .ThenInclude(i => i.VolunteerRequirement)
                .ThenInclude(i => i.VolunteerProfile)
                .ToListAsync();

            return requirements;
        }
        //Getting Volunteer Requirements
        public async Task<IEnumerable<VolunteerRequirement>> GetVolunteerRequirements(int volunteerId)
        {
            var requirements = await _studentContext.VolunteerRequirements.Where(id => id.VolunteerProfileId == volunteerId)
                .Include(i => i.OneToOnes)
                .ThenInclude(i => i.StudentProfile)
                .Include(i => i.InGroupVolunteer)
                .ThenInclude(i => i.StudentProfile)
                .ToListAsync();

            return requirements;
        }

        //Gets the list of all the Volunteer Requirements except those in which 
        //student is enrolled
        public async Task<IEnumerable<VolunteerRequirement>> ShowAllVolunteerDetailsForStudent(int studentId)
        {
            //Query to get all the oneToOne classes in which the student is enrolled
            var checkOneToOne =  _studentContext.OneToOneClass.Where(id => id.StudentProfileId == studentId)
                .Select(vid =>  vid.VolunteerRequirement)
                .ToList();

            //Query to get all the group classes in which the student is enrolled
            var isInGroup = _studentContext.GroupsClass.Where(id => id.StudentProfileId == studentId)
               .Select(vid => vid.VolunteerRequirement)
               .ToList();

            //query to get all the requirements
            var r = await _studentContext.VolunteerRequirements
                .Include(i => i.VolunteerProfile)
                .ToListAsync();


            if (isInGroup != null || checkOneToOne != null)
            {
                //here the requirements in which the student is enrolled are excluded
                IEnumerable<VolunteerRequirement> requirement = r.Except(isInGroup);
                requirement = requirement.Except(checkOneToOne);
                return requirement;
            }
            return null;
        }


        //Gets the list of all the Student Requirements except those which the 
        //Volunteer has accepted
        public async Task<IEnumerable<StudentRequirement>> ShowAllStudentRequirment(int volunteerId)
        {
            //Query to get all the oneToOne classes in which the volunteer has accepted
            var checkOneToOne = _studentContext.OneToOneClass.Where(id => id.VolunteerProfileId == volunteerId)
                .Select(vid => vid.StudentRequirement)
                .ToList();

            var checkGroup = _studentContext.GroupsClass
                .Where(id => id.VolunteerProfileId == volunteerId)
                .Select(sid => sid.StudentRequirement)
                .ToList();
            //Gets all the student requirements
            var r = await _studentContext.StudentRequirements.Include(i => i.StudentProfile).ToListAsync();


            if (checkOneToOne != null)
            {
                //excludes the student requirements those which the volunteer has already accepted
                IEnumerable<StudentRequirement> requirement = r.Except(checkOneToOne);
                requirement = requirement.Except(checkGroup);
                return requirement;
            }
            return null;

        }

        public async Task<string> UpdateStudentRequirement(int studentReqId, StudentRequirementViewModel requirementViewModel)
        {
            var studentReq = await _studentContext.StudentRequirements.FirstOrDefaultAsync(id => id.Id == studentReqId);

            if(studentReq == null)
            {
                return "Requirement doesn't exist";
            }

            studentReq.StartDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.StartDay);
            studentReq.EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay);
            studentReq.StartTime = requirementViewModel.StartTime;
            studentReq.EndTime = requirementViewModel.EndTime;
            studentReq.Subject = requirementViewModel.Subject;
            studentReq.Topic = requirementViewModel.Topic;
            studentReq.TypeOfClass = (TypeOfClass)Enum.Parse(typeof(TypeOfClass), requirementViewModel.TypeOfClass);
            
            SaveChangesToDB();

            return "Requirement Updated";
        }

        public async Task<string> UpdateVolunteerRequirement(int volunteerReqId, VolunteerRequirementViewModel requirementViewModel)
        {
            var volunteerReq = await _studentContext.VolunteerRequirements.FirstOrDefaultAsync(id=>id.Id == volunteerReqId);

            if (volunteerReq == null)
            {
                return "Requirement doesn't exist";
            }

            volunteerReq.StartDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.StartDay);
            volunteerReq.EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay);
            volunteerReq.StartTime = requirementViewModel.StartTime;
            volunteerReq.EndTime = requirementViewModel.EndTime;
            volunteerReq.Subject = requirementViewModel.Subject;
            volunteerReq.Topic = requirementViewModel.Topic;

            SaveChangesToDB();

            return "Requirement Updated";
        }


        #region PRIVATE HELPER METHODS

        //This query gets all the student requirement for the particular studnet 
        //which have same timings so that the requirements timing dont clash
        //throws an exception if the days clash
        private async Task<DaysTimeClashViewModel> CheckIfStudentDaysClash(StudentRequirementViewModel studentRequirement)
        {
            //this query gets all the existing requirements which have similar timings to the new one
            var getRequirementWithSimilarTimings = await _studentContext.StudentRequirements
                 .Where(student => student.StudentProfileId == studentRequirement.ProfileId &&
                     student.StartTime == studentRequirement.StartTime &&
                     student.EndTime == studentRequirement.EndTime)
                 .Select(id => new DaysTimeClashViewModel{ Id = id.Id , StartDay = id.StartDay, EndDay = id.EndDay, Topic = id.Topic, Subject = id.Subject})
                 .ToListAsync();

            var newStartDay = (int)Enum.Parse(typeof(Days), studentRequirement.StartDay);
            var newEndDay = (int)Enum.Parse(typeof(Days) , studentRequirement.EndDay);
            
            //here it checks if the days are overlapping for the same timing
            foreach(var s in getRequirementWithSimilarTimings)
            {
                var existingStartDay = (int)s.StartDay;
                var existingEndDay = (int)s.EndDay;
                if((existingStartDay <= newStartDay && newStartDay <= existingEndDay) ||
                    (existingStartDay <= newEndDay && newEndDay <= existingEndDay))
                {
                    //if the days are clashing throws an exception
                    return s;
                    throw new DaysClashingException($"The selected days clash with the schedule for {s.Topic} class");
                }
            }
            return null;
        }

        private async Task<DaysTimeClashViewModel> CheckIfVolunteerDaysClash(VolunteerRequirementViewModel requirementViewModel)
        {
            var getRequirementWithSimilarTimings = await _studentContext.VolunteerRequirements
                .Where(v => v.VolunteerProfileId == requirementViewModel.ProfileId &&
                    v.StartTime == requirementViewModel.StartTime &&
                    v.EndTime == requirementViewModel.EndTime)
                .Select(id => new DaysTimeClashViewModel { Id = id.Id, StartDay = id.StartDay, EndDay = id.EndDay, Topic = id.Topic, Subject = id.Subject })
                .ToListAsync();

            var newStartDay = (int)Enum.Parse(typeof(Days), requirementViewModel.StartDay);
            var newEndDay = (int)Enum.Parse(typeof(Days), requirementViewModel.EndDay);

            foreach (var s in getRequirementWithSimilarTimings)
            {
                var existingStartDay = (int)s.StartDay;
                var existingEndDay = (int)s.EndDay;
                if ((existingStartDay <= newStartDay && newStartDay <= existingEndDay) ||
                    (existingStartDay <= newEndDay && newEndDay <= existingEndDay))
                {
                    //if the days are clashing throws an exception
                    return s;
                    throw new DaysClashingException($"The selected days clash with the schedule for  class");
                }
            }
            return null;
        }

        private DateTime GetDateTime()
        {
            DateTime serverTime = DateTime.Now; 
            DateTime utcTime = serverTime.ToUniversalTime(); 

            TimeZoneInfo tzi = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, tzi);
            return localTime;
        }
        private void SaveChangesToDB()
        {
            _studentContext.SaveChanges();
        }
        #endregion
    }
}
