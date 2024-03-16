using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MigAddCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CourseEpisodes",
                columns: table => new
                {
                    EpisodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    CourseTitle = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
                    EpisodeTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    episodeFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsFree = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseEpisodes", x => x.EpisodeId);
                    table.ForeignKey(
                        name: "FK_CourseEpisodes_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEpisodes_CourseId",
                table: "CourseEpisodes",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CourseEpisodes");
        }
    }
}
