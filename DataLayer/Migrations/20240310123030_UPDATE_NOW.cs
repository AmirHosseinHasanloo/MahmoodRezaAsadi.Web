using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class UPDATENOW : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups");

            migrationBuilder.DropColumn(
                name: "CourseGroupGroupId",
                table: "CourseGroups");

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups",
                column: "ParentId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups");

            migrationBuilder.AddColumn<int>(
                name: "CourseGroupGroupId",
                table: "CourseGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups",
                column: "CourseGroupGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseGroups_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups",
                column: "CourseGroupGroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId");
        }
    }
}
