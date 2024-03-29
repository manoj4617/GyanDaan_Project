﻿using System;
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
            public Gender Gender { get; set; }
            public string MobileNumber { get; set; }
            public string Email { get; set; }
            public EducationQualification EducationQualification { get; set; }
            public StudentAccount StudentAccount { get; set; }
            public IList<StudentRequirement> StudentRequirements { get; set; }
            public IList<OneToOne> OneToOne { get; set; }
            public IList<Group> InGroupStudent { get; set; }
            public List<VolunteerInbox> VolunteerInboxes { get; set; }
            public List<StudentInbox> StudentInboxes { get; set; }
        }

        public class StudentAccount
        {
            public int Id { get; set; }
            public int StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }
            public DateTime JoinedOn { get; set; }
            public DateTime DateOfBirth { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PassowrdSalt { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public long Pin { get; set; }
            public bool IsVolunteer { get; set; }
        }

        public class StudentRequirement
        {
            public int Id { get; set; }
            public int StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }
            public string PostedOnDate { get; set; }
            public Days StartDay { get; set; }
            public Days EndDay { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            public string Subject { get; set; }
            public string Topic { get; set; }
            public DateTime TimeOfStart { get; set; }
            public TypeOfClass TypeOfClass { get; set; }
            public bool AcceptedByVolunteer { get; set; }
            public OneToOne OneToOne { get; set; }
            public Group Group { get; set; }
            public StudentInbox StudentInbox { get; set; }
        }

        public class VolunteerProfile
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public string MobileNumber { get; set; }
            public string Email { get; set; }
            public VolunteerAccount VolunteerAccount { get; set; }
            public EducationQualification EducationQualification { get; set; }
            public List<VolunteerRequirement> VolunteerRequirements { get; set; }
            public List<OneToOne> OneToOnes { get; set; }
            public List<Group> Groups { get; set; }
            public List<VolunteerInbox> VolunteerInboxes { get; set; }
            public List<StudentInbox> StudentInboxes { get; set; }
        }

        public class VolunteerAccount
        {
            public int Id { get; set; }
            public int VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public DateTime JoinedOn { get; set; }
            public DateTime DateOfBirth { get; set; }
            public byte[] PasswordHash { get; set; }
            public byte[] PassowrdSalt { get; set; }
            public string Street { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public long Pin { get; set; }
        }

        public class VolunteerRequirement
        {
            public int Id { get; set; }
            public int VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public string PostedOnDate { get; set; }
            public string Subject { get; set; }
            public string Topic { get; set; }
            public TypeOfClass TypeOfClass { get; set; }
            public Days StartDay { get; set; }
            public Days EndDay { get; set; }
            public string StartTime { get; set; }
            public string EndTime { get; set; }
            /*public int MaxLimit { get; set; }*/
            public OneToOne OneToOnes { get; set; }
            public List<Group> InGroupVolunteer { get; set; }
            public List<VolunteerInbox> VolunteerInboxes { get; set; }
            public List<StudentInbox> StudentInboxes { get; set; }
        }

        public class OneToOne
        {
            public int Id { get; set; }

            //Populated when Volunteer posts the request
            public int? StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }

            //Populated when Student posts the request
            public int? StudentRequirementId { get; set; }
            public StudentRequirement StudentRequirement { get; set; }

            //Populated when Student posts the request
            public int? VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }

            //Populated when volunteer posts the request
            public int? VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
        }

        public class Group
        {
            public int Id { get; set; }
            public int? VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public int? VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
            public int?  StudentRequirementId { get; set; }
            public StudentRequirement StudentRequirement { get; set; }
            public int? StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }
        }

        public class  VolunteerInbox
        {
            public int Id { get; set; }
            public int? VolunteerProfileId { get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public int? VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
            public int? StudentProfileId { get; set; }
            public StudentProfile StudentProfile { get; set; }
        }

        public class StudentInbox
        {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public StudentProfile StudentProfile { get; set; }
            public int StudentRequirementId { get; set; }
            public StudentRequirement StudentRequirement { get; set; }
            public int VolunteerId{ get; set; }
            public VolunteerProfile VolunteerProfile { get; set; }
            public int VolunteerRequirementId { get; set; }
            public VolunteerRequirement VolunteerRequirement { get; set; }
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
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday
        }

        public enum Gender
        {
            Female,
            Male,
            Other
        }
        #endregion
    }
}
