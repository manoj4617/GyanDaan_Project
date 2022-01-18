using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class AddedSubjectAndTopic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AreaOfSpecialization",
                table: "VolunteerRequirements");

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "VolunteerRequirements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Topic",
                table: "VolunteerRequirements",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subject",
                table: "StudentRequirements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subject",
                table: "VolunteerRequirements");

            migrationBuilder.DropColumn(
                name: "Topic",
                table: "VolunteerRequirements");

            migrationBuilder.DropColumn(
                name: "Subject",
                table: "StudentRequirements");

            migrationBuilder.AddColumn<string>(
                name: "AreaOfSpecialization",
                table: "VolunteerRequirements",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
