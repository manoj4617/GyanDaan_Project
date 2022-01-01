using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class MadeChangesToTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerId",
                table: "VolunteerInboxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VolunteerProfileId",
                table: "VolunteerInboxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerInboxes_VolunteerProfileId",
                table: "VolunteerInboxes",
                column: "VolunteerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VolunteerInboxes_VolunteerProfiles_VolunteerProfileId",
                table: "VolunteerInboxes");

            migrationBuilder.DropIndex(
                name: "IX_VolunteerInboxes_VolunteerProfileId",
                table: "VolunteerInboxes");

            migrationBuilder.DropColumn(
                name: "VolunteerId",
                table: "VolunteerInboxes");

            migrationBuilder.DropColumn(
                name: "VolunteerProfileId",
                table: "VolunteerInboxes");
        }
    }
}
