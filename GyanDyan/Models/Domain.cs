using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using static GyanDyan.Models.Domain;

namespace GyanDyan.Models
{
    public class Domain
    {
        public class StudentProfile
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MobileNumber { get; set; }
            public DateTime JoinedOn { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Email { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PassowrdSalt { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public long Pin { get; set; }
            public EducationQualification EducationQualification { get; set; }
            public IList<StudentRequirement> StudentRequirements { get; set; }
            public IList<OneToOne> OneToOne { get; set; }
            public IList<Group> InGroupStudent { get; set; }
        }

        public class StudentRequirement
        {
            public int Id { get; set; }
            public int StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }
            public DateTime PostedOnDate { get; set; }
            public Days StartDay { get; set; }
            public Days EndDay { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Topic { get; set; }
            public DateTime TimeOfStart { get; set; }
            public TypeOfClass TypeOfClass { get; set; }
            public bool AcceptedByVolunteer { get; set; }
            public OneToOne OneToOne { get; set; }

        }

        public class VolunteerProfile
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MobileNumber { get; set; }
            public DateTime JoinedOn { get; set; }
            public DateTime DateOfBirth { get; set; }
            public string Email { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PassowrdSalt { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public long Pin { get; set; }
            public EducationQualification EducationQualification { get; set; }
            public IList<VolunteerRequirement> VolunteerRequirements { get; set; }
            public IList<OneToOne> OneToOnes { get; set; }
        }

        public class VolunteerRequirement
        {
            public int Id { get; set; }
            public int VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public DateTime PostedOnDate { get; set; }
            public string AreaOfSpecialization { get; set; }
            public TypeOfClass TypeOfClass { get; set; }
            public Days StartDay { get; set; }
            public Days EndDay { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public OneToOne OneToOnes { get; set; }
            public Group InGroupVolunteer { get; set; }
        }

        public class OneToOne
        {
            public int Id { get; set; }

            //Populated when Volunteer posts the request
            public int? StudentId { get; set; }
            public StudentProfile StudentProfile { get; set; }

            //Populated when Student posts the request
            public int? StudentRequirementId { get; set; }
            public StudentRequirement StudentRequirement { get; set; }

            //Populated when Student posts the request
            public int? VolunteerId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }

            //Populated when volunteer posts the request
            public int? VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
        }

        public class Group
        {
            public int Id { get; set; }
            public int? VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
            public int? StudentId { get; set; }
            public StudentProfile StudentProfile { get; set; }
        }

        #region <ENUMS>
        public enum EducationQualification
        {
            UnderGraduate,
            Graduate,
            PostGraduate
        }

        public enum TypeOfClass
        {
            OneToOne,
            Group
        }
        public enum Days
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
            Sunday
        }
        #endregion
    }
}
