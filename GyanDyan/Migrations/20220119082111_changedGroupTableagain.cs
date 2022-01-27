using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class changedGroupTableagain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerProfileId",
                table: "GroupsClass",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_VolunteerProfileId",
                table: "GroupsClass",
                column: "VolunteerProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass",
                column: "VolunteerProfileId",
                principalTable: "VolunteerProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_VolunteerProfiles_VolunteerProfileId",
                table: "GroupsClass");

            migrationBuilder.DropIndex(
                name: "IX_GroupsClass_VolunteerProfileId",
                table: "GroupsClass");

            migrationBuilder.DropColumn(
                name: "VolunteerProfileId",
                table: "GroupsClass");
        }
    }
}
