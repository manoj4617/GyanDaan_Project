using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class addedStudentInboxTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentInboxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentRequirementId = table.Column<int>(nullable: false),
                    VolunteerId = table.Column<int>(nullable: false),
                    VolunteerProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentInboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentInboxes_StudentRequirements_StudentRequirementId",
                        column: x => x.StudentRequirementId,
                        principalTable: "StudentRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentInboxes_VolunteerProfiles_VolunteerProfileId",
                        column: x => x.VolunteerProfileId,
                        principalTable: "VolunteerProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentInboxes_StudentRequirementId",
                table: "StudentInboxes",
                column: "StudentRequirementId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentInboxes_VolunteerProfileId",
                table: "StudentInboxes",
                column: "VolunteerProfileId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentInboxes");
        }
    }
}
