using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class ChangedVolunteerTableMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_VolunteerProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerRequirements_StudentProfiles_VolunteerProfileId",
                table: "VolunteerRequirements");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "StudentProfiles");

            migrationBuilder.CreateTable(
                name: "VolunteerProfiles",
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
                    EducationQualification = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerProfiles", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerRequirements_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerRequirements",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerRequirements_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerRequirements");

            migrationBuilder.DropTable(
                name: "VolunteerProfiles");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_VolunteerProfileId",
                table: "OneToOneClass",
                column: "VolunteerProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerRequirements_StudentProfiles_VolunteerProfileId",
                table: "VolunteerRequirements",
                column: "VolunteerProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
