using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class updatedStudentInboxTableAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "StudentInboxes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StudentProfileId",
                table: "StudentInboxes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInboxes_StudentProfileId",
                table: "StudentInboxes",
                column: "StudentProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentInboxes_StudentProfiles_StudentProfileId",
                table: "StudentInboxes",
                column: "StudentProfileId",
                principalTable: "StudentProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentInboxes_StudentProfiles_StudentProfileId",
                table: "StudentInboxes");

            migrationBuilder.DropIndex(
                name: "IX_StudentInboxes_StudentProfileId",
                table: "StudentInboxes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentInboxes");

            migrationBuilder.DropColumn(
                name: "StudentProfileId",
                table: "StudentInboxes");
        }
    }
}
