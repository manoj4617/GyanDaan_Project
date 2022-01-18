using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class updatedStudentInboxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VolunteerRequirementId",
                table: "StudentInboxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInboxes_VolunteerRequirementId",
                table: "StudentInboxes",
                column: "VolunteerRequirementId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "StudentInboxes",
                column: "VolunteerRequirementId",
                principalTable: "VolunteerRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInboxes_VolunteerRequirements_VolunteerRequirementId",
                table: "StudentInboxes");

            migrationBuilder.DropIndex(
                name: "IX_StudentInboxes_VolunteerRequirementId",
                table: "StudentInboxes");

            migrationBuilder.DropColumn(
                name: "VolunteerRequirementId",
                table: "StudentInboxes");
        }
    }
}
