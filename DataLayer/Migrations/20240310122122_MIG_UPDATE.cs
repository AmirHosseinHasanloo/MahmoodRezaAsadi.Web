using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MIGUPDATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_ParentId",
                table: "CourseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_GroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_StatusId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_GroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_StatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroups_ParentId",
                table: "CourseGroups");

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
                name: "UserId",
                table: "Courses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CourseGroupGroupId",
                table: "CourseGroups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseGroupGroupId",
                table: "Courses",
                column: "CourseGroupGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses",
                column: "CourseStatusStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_UserId",
                table: "Courses",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseGroups_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseGroups_SubGroupId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_CourseStatuses_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Users_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_UserId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_CourseGroups_CourseGroupGroupId",
                table: "CourseGroups");

            migrationBuilder.DropColumn(
                name: "CourseGroupGroupId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseStatusStatusId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseGroupGroupId",
                table: "CourseGroups");

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

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Users_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
