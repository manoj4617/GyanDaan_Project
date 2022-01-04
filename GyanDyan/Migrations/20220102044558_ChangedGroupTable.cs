using Microsoft.EntityFrameworkCore.Migrations;

namespace GyanDyan.Migrations
{
    public partial class ChangedGroupTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentRequirementId",
                table: "GroupsClass",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupsClass_StudentRequirementId",
                table: "GroupsClass",
                column: "StudentRequirementId",
                unique: true,
                filter: "[StudentRequirementId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupsClass_StudentRequirements_StudentRequirementId",
                table: "GroupsClass",
                column: "StudentRequirementId",
                principalTable: "StudentRequirements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupsClass_StudentRequirements_StudentRequirementId",
                table: "GroupsClass");

            migrationBuilder.DropIndex(
                name: "IX_GroupsClass_StudentRequirementId",
                table: "GroupsClass");

            migrationBuilder.DropColumn(
                name: "StudentRequirementId",
                table: "GroupsClass");
        }
    }
}
