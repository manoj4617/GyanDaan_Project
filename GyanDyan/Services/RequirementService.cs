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

        public async Task AddNewStudentRequirement(StudentRequirementViewModel requirementViewModel)
        {
            //This query gets all the student requirement for the particular studnet 
            //which have same timings so that the requirements timing dont clash
            await CheckIfStudentDaysClash(requirementViewModel);
          
            var newStudentRequirement = new StudentRequirement()
            {
                StudentProfileId = requirementViewModel.StudentProfileId,
                PostedOnDate = DateTime.Now,
                StartDay = (Days)Enum.Parse(typeof(Days),requirementViewModel.StartDay),
                EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay),
                StartTime = requirementViewModel.StartTime,
                EndTime = requirementViewModel.EndTime,
                Topic = requirementViewModel.Topic,
                TypeOfClass = (TypeOfClass)Enum.Parse(typeof(TypeOfClass),requirementViewModel.TypeOfClass)
            };

            await _studentContext.StudentRequirements.AddAsync(newStudentRequirement);

            SaveChangesToDB();
        }

        public async Task AddNewVolunteerRequirement(VolunteerRequirementViewModel requirementViewModel)
        {
            await CheckIfVolunteerDaysClash(requirementViewModel);

            var newVolunteerRequirement = new VolunteerRequirement()
            {
                VolunteerProfileId = requirementViewModel.VolunteerProfileId,
                PostedOnDate = DateTime.Now,
                StartDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.StartDay),
                EndDay = (Days)Enum.Parse(typeof(Days), requirementViewModel.EndDay),
                StartTime = requirementViewModel.StartTime,
                EndTime = requirementViewModel.EndTime,
                AreaOfSpecialization = requirementViewModel.AreaOfspecialization,
                TypeOfClass = (TypeOfClass)Enum.Parse(typeof(TypeOfClass), requirementViewModel.TypeOfClass)
            };

            await _studentContext.VolunteerRequirements.AddAsync(newVolunteerRequirement);

            SaveChangesToDB();
        }

        //Getting Student Requirements

        //Getting Volunteer Requirements

      

        #region PRIVATE HELPER METHODS

        //This query gets all the student requirement for the particular studnet 
        //which have same timings so that the requirements timing dont clash
        //throws an exception if the days clash
        private async Task CheckIfStudentDaysClash(StudentRequirementViewModel studentRequirement)
        {
            //this query gets all the existing requirements which have similar timings to the new one
            var getRequirementWithSimilarTimings = await _studentContext.StudentRequirements
                 .Where(student => student.StudentProfileId == studentRequirement.StudentProfileId &&
                     student.StartTime == studentRequirement.StartTime &&
                     student.EndTime == studentRequirement.EndTime)
                 .Select(id => new { id.Id , id.StartDay, id.EndDay, id.Topic})
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
                    throw new DaysClashingException($"The selected days clash with the schedule for {s.Topic} class");
                }
            }
        }

        private async Task CheckIfVolunteerDaysClash(VolunteerRequirementViewModel requirementViewModel)
        {
            var getRequirementWithSimilarTimings = await _studentContext.VolunteerRequirements
                .Where(v => v.VolunteerProfileId == requirementViewModel.VolunteerProfileId &&
                    v.StartTime == requirementViewModel.StartTime &&
                    v.EndTime == requirementViewModel.EndTime)
                .Select(v => new { v.Id, v.StartDay, v.EndDay, v.AreaOfSpecialization })
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
                    throw new DaysClashingException($"The selected days clash with the schedule for {s.AreaOfSpecialization} class");
                }
            }
        }

        private void SaveChangesToDB()
        {
            _studentContext.SaveChanges();
        }
        #endregion
    }
}
