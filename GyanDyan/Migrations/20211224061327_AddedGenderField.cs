using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class AddedGenderField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "VolunteerProfiles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "StudentProfiles",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "VolunteerProfiles");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "StudentProfiles");
        }
    }
}
