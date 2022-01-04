using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class AddedNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VolunteerInboxes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolunteerRequirementId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    StudentProfileId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerInboxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VolunteerInboxes_StudentProfiles_StudentProfileId",
                        column: x => x.StudentProfileId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VolunteerInboxes_VolunteerRequirements_VolunteerRequirementId",
                        column: x => x.VolunteerRequirementId,
                        principalTable: "VolunteerRequirements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerInboxes_StudentProfileId",
                table: "VolunteerInboxes",
                column: "StudentProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerInboxes_VolunteerRequirementId",
                table: "VolunteerInboxes",
                column: "VolunteerRequirementId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerInboxes");
        }
    }
}
