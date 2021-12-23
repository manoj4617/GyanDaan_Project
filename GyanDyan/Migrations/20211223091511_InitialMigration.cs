using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    MobileNumber = table.Column<string>(nullable: true),
                    JoinedOn = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PassowrdSalt = table.Column<byte[]>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Pin = table.Column<long>(nullable: false),
                    EducationQualification = table.Column<int>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StudentRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(nullable: false),
                    PostedOnDate = table.Column<DateTime>(nullable: false),
                    StartDay = table.Column<int>(nullable: false),
                    EndDay = table.Column<int>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    TimeOfStart = table.Column<DateTime>(nullable: false),
                    TypeOfClass = table.Column<int>(nullable: false),
                    AcceptedByVolunteer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentRequirements_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerRequirements",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerProfileId = table.Column<int>(nullable: false),
                    PostedOnDate = table.Column<DateTime>(nullable: false),
                    AreaOfSpecialization = table.Column<string>(nullable: true),
                    TypeOfClass = table.Column<int>(nullable: false),
                    StartDay = table.Column<int>(nullable: false),
                    EndDay = table.Column<int>(nullable: false),
                    StartTime = table.Column<string>(nullable: true),
                    EndTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerRequirements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerRequirements_StudentProfiles_VolunteerProfileId",
                        column: x => x.VolunteerProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupsClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerRequirementId = table.Column<int>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    StudentProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupsClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GroupsClass_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GroupsClass_VolunteerRequirements_VolunteerRequirementId",
                        column: x => x.VolunteerRequirementId,
                        principalTable: "VolunteerRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OneToOneClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: true),
                    StudentProfileId = table.Column<int>(nullable: true),
                    StudentRequirementId = table.Column<int>(nullable: true),
                    VolunteerId = table.Column<int>(nullable: true),
                    VolunteerProfileId = table.Column<int>(nullable: true),
                    VolunteerRequirementId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OneToOneClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OneToOneClass_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OneToOneClass_StudentRequirements_StudentRequirementId",
                        column: x => x.StudentRequirementId,
                        principalTable: "StudentRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OneToOneClass_StudentProfiles_VolunteerProfileId",
                        column: x => x.VolunteerProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OneToOneClass_VolunteerRequirements_VolunteerRequirementId",
                        column: x => x.VolunteerRequirementId,
                        principalTable: "VolunteerRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_StudentProfileId",
                table: "GroupsClass",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_VolunteerRequirementId",
                table: "GroupsClass",
                column: "VolunteerRequirementId",
                unique: true,
                filter: "[VolunteerRequirementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OneToOneClass_StudentProfileId",
                table: "OneToOneClass",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OneToOneClass_StudentRequirementId",
                table: "OneToOneClass",
                column: "StudentRequirementId",
                unique: true,
                filter: "[StudentRequirementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_OneToOneClass_VolunteerProfileId",
                table: "OneToOneClass",
                column: "VolunteerProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_OneToOneClass_VolunteerRequirementId",
                table: "OneToOneClass",
                column: "VolunteerRequirementId",
                unique: true,
                filter: "[VolunteerRequirementId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_StudentRequirements_StudentProfileId",
                table: "StudentRequirements",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerRequirements_VolunteerProfileId",
                table: "VolunteerRequirements",
                column: "VolunteerProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupsClass");

            migrationBuilder.DropTable(
                name: "OneToOneClass");

            migrationBuilder.DropTable(
                name: "StudentRequirements");

            migrationBuilder.DropTable(
                name: "VolunteerRequirements");

            migrationBuilder.DropTable(
                name: "StudentProfiles");
        }
    }
}
