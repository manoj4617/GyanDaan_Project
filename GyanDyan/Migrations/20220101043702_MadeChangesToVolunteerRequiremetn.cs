using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class MadeChangesToVolunteerRequiremetn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupsClass_VolunteerRequirementId",
                table: "GroupsClass");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_VolunteerRequirementId",
                table: "GroupsClass",
                column: "VolunteerRequirementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GroupsClass_VolunteerRequirementId",
                table: "GroupsClass");

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_VolunteerRequirementId",
                table: "GroupsClass",
                column: "VolunteerRequirementId",
                unique: true,
                filter: "[VolunteerRequirementId] IS NOT NULL");
        }
    }
}
