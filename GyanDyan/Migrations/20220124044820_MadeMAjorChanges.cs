using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class MadeMAjorChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_StudentProfiles_StudentProfileId",
                table: "GroupsClass");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_StudentProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "VolunteerInboxes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "JoinedOn",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "PassowrdSalt",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "State",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "VolunteerInboxes");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "VolunteerInboxes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "IsVolunteer",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "JoinedOn",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "PassowrdSalt",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Pin",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "State",
                table: "StudentProfiles");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "StudentProfiles");

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerRequirementId",
                table: "VolunteerInboxes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "StudentAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentProfileId = table.Column<int>(nullable: false),
                    JoinedOn = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PassowrdSalt = table.Column<byte[]>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Pin = table.Column<long>(nullable: false),
                    IsVolunteer = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAccounts_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VolunteerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerProfileId = table.Column<int>(nullable: false),
                    JoinedOn = table.Column<DateTime>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PassowrdSalt = table.Column<byte[]>(nullable: true),
                    Street = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Pin = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerAccounts_VolunteerProfiles_VolunteerProfileId",
                        column: x => x.VolunteerProfileId,
                        principalTable: "VolunteerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentAccounts_StudentProfileId",
                table: "StudentAccounts",
                column: "StudentProfileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerAccounts_VolunteerProfileId",
                table: "VolunteerAccounts",
                column: "VolunteerProfileId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_StudentProfiles_StudentProfileId",
                table: "GroupsClass",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_StudentProfileId",
                table: "OneToOneClass",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "VolunteerInboxes",
                column: "VolunteerRequirementId",
                principalTable: "VolunteerRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_StudentProfiles_StudentProfileId",
                table: "GroupsClass");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_StudentProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes");

            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "VolunteerInboxes");

            migrationBuilder.DropTable(
                name: "StudentAccounts");

            migrationBuilder.DropTable(
                name: "VolunteerAccounts");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "VolunteerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "VolunteerProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedOn",
                table: "VolunteerProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "PassowrdSalt",
                table: "VolunteerProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "VolunteerProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Pin",
                table: "VolunteerProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "VolunteerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "VolunteerProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VolunteerRequirementId",
                table: "VolunteerInboxes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "VolunteerInboxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "VolunteerInboxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsVolunteer",
                table: "StudentProfiles",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedOn",
                table: "StudentProfiles",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<byte[]>(
                name: "PassowrdSalt",
                table: "StudentProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "StudentProfiles",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Pin",
                table: "StudentProfiles",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "StudentProfiles",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_StudentProfiles_StudentProfileId",
                table: "GroupsClass",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_StudentProfiles_StudentProfileId",
                table: "OneToOneClass",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OneToOneClass_VolunteerProfiles_VolunteerProfileId",
                table: "OneToOneClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "VolunteerInboxes",
                column: "VolunteerRequirementId",
                principalTable: "VolunteerRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
