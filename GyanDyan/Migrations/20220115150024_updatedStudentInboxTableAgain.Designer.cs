﻿// <auto-generated />
using System;
using GyanDyan.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GyanDyan.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220115150024_updatedStudentInboxTableAgain")]
    partial class updatedStudentInboxTableAgain
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GyanDyan.Models.Domain+Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentProfileId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentRequirementId")
                        .HasColumnType("int");

                    b.Property<int?>("VolunteerRequirementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentProfileId");

                    b.HasIndex("StudentRequirementId")
                        .IsUnique()
                        .HasFilter("[StudentRequirementId] IS NOT NULL");

                    b.HasIndex("VolunteerRequirementId");

                    b.ToTable("GroupsClass");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+OneToOne", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentProfileId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentRequirementId")
                        .HasColumnType("int");

                    b.Property<int?>("VolunteerProfileId")
                        .HasColumnType("int");

                    b.Property<int?>("VolunteerRequirementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentProfileId");

                    b.HasIndex("StudentRequirementId")
                        .IsUnique()
                        .HasFilter("[StudentRequirementId] IS NOT NULL");

                    b.HasIndex("VolunteerProfileId");

                    b.HasIndex("VolunteerRequirementId")
                        .IsUnique()
                        .HasFilter("[VolunteerRequirementId] IS NOT NULL");

                    b.ToTable("OneToOneClass");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+StudentInbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentProfileId")
                        .HasColumnType("int");

                    b.Property<int>("StudentRequirementId")
                        .HasColumnType("int");

                    b.Property<int>("VolunteerId")
                        .HasColumnType("int");

                    b.Property<int?>("VolunteerProfileId")
                        .HasColumnType("int");

                    b.Property<int>("VolunteerRequirementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentProfileId");

                    b.HasIndex("StudentRequirementId")
                        .IsUnique();

                    b.HasIndex("VolunteerProfileId");

                    b.HasIndex("VolunteerRequirementId");

                    b.ToTable("StudentInboxes");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+StudentProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("EducationQualification")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("IsVolunteer")
                        .HasColumnType("bit");

                    b.Property<DateTime>("JoinedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PassowrdSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("Pin")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StudentProfiles");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+StudentRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("AcceptedByVolunteer")
                        .HasColumnType("bit");

                    b.Property<int>("EndDay")
                        .HasColumnType("int");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostedOnDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StartDay")
                        .HasColumnType("int");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StudentProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeOfStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOfClass")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentProfileId");

                    b.ToTable("StudentRequirements");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+VolunteerInbox", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<int?>("StudentProfileId")
                        .HasColumnType("int");

                    b.Property<int>("VolunteerId")
                        .HasColumnType("int");

                    b.Property<int?>("VolunteerProfileId")
                        .HasColumnType("int");

                    b.Property<int>("VolunteerRequirementId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentProfileId");

                    b.HasIndex("VolunteerProfileId");

                    b.HasIndex("VolunteerRequirementId");

                    b.ToTable("VolunteerInboxes");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+VolunteerProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int>("EducationQualification")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<DateTime>("JoinedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MobileNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PassowrdSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<long>("Pin")
                        .HasColumnType("bigint");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("VolunteerProfiles");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+VolunteerRequirement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EndDay")
                        .HasColumnType("int");

                    b.Property<string>("EndTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostedOnDate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StartDay")
                        .HasColumnType("int");

                    b.Property<string>("StartTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Topic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOfClass")
                        .HasColumnType("int");

                    b.Property<int>("VolunteerProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VolunteerProfileId");

                    b.ToTable("VolunteerRequirements");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+Group", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+StudentProfile", "StudentProfile")
                        .WithMany("InGroupStudent")
                        .HasForeignKey("StudentProfileId");

                    b.HasOne("GyanDyan.Models.Domain+StudentRequirement", "StudentRequirement")
                        .WithOne("Group")
                        .HasForeignKey("GyanDyan.Models.Domain+Group", "StudentRequirementId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerRequirement", "VolunteerRequirement")
                        .WithMany("InGroupVolunteer")
                        .HasForeignKey("VolunteerRequirementId");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+OneToOne", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+StudentProfile", "StudentProfile")
                        .WithMany("OneToOne")
                        .HasForeignKey("StudentProfileId");

                    b.HasOne("GyanDyan.Models.Domain+StudentRequirement", "StudentRequirement")
                        .WithOne("OneToOne")
                        .HasForeignKey("GyanDyan.Models.Domain+OneToOne", "StudentRequirementId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerProfile", "VolunteerProfile")
                        .WithMany("OneToOnes")
                        .HasForeignKey("VolunteerProfileId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerRequirement", "VolunteerRequirement")
                        .WithOne("OneToOnes")
                        .HasForeignKey("GyanDyan.Models.Domain+OneToOne", "VolunteerRequirementId");
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+StudentInbox", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+StudentProfile", "StudentProfile")
                        .WithMany("StudentInboxes")
                        .HasForeignKey("StudentProfileId");

                    b.HasOne("GyanDyan.Models.Domain+StudentRequirement", "StudentRequirement")
                        .WithOne("StudentInbox")
                        .HasForeignKey("GyanDyan.Models.Domain+StudentInbox", "StudentRequirementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GyanDyan.Models.Domain+VolunteerProfile", "VolunteerProfile")
                        .WithMany("StudentInboxes")
                        .HasForeignKey("VolunteerProfileId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerRequirement", "VolunteerRequirement")
                        .WithMany("StudentInboxes")
                        .HasForeignKey("VolunteerRequirementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+StudentRequirement", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+StudentProfile", "StudentProfile")
                        .WithMany("StudentRequirements")
                        .HasForeignKey("StudentProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+VolunteerInbox", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+StudentProfile", "StudentProfile")
                        .WithMany("VolunteerInboxes")
                        .HasForeignKey("StudentProfileId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerProfile", "VolunteerProfile")
                        .WithMany("VolunteerInboxes")
                        .HasForeignKey("VolunteerProfileId");

                    b.HasOne("GyanDyan.Models.Domain+VolunteerRequirement", "VolunteerRequirement")
                        .WithMany("VolunteerInboxes")
                        .HasForeignKey("VolunteerRequirementId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GyanDyan.Models.Domain+VolunteerRequirement", b =>
                {
                    b.HasOne("GyanDyan.Models.Domain+VolunteerProfile", "VolunteerProfile")
                        .WithMany("VolunteerRequirements")
                        .HasForeignKey("VolunteerProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
