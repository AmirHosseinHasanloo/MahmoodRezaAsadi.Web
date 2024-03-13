using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MigChangeAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "TeacherId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "SubGroupId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_GroupId",
                table: "Courses",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_StatusId",
                table: "Courses",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses",
                column: "GroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses",
                column: "SubGroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_StatusId",
                table: "Courses",
                column: "StatusId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_StatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_GroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StatusId",
                table: "Courses");

            migrationBuilder.AlterColumn<int>(
                name: "SubGroupId",
                table: "Courses",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CourseGroupGroupId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseStatusStatusId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TeacherId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseGroupGroupId",
                table: "Courses",
                column: "CourseGroupGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses",
                column: "CourseStatusStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_CourseGroupGroupId",
                table: "Courses",
                column: "CourseGroupGroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses",
                column: "SubGroupId",
                principalTable: "CourseGroups",
                principalColumn: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusStatusId",
                table: "Courses",
                column: "CourseStatusStatusId",
                principalTable: "CourseStatuses",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
