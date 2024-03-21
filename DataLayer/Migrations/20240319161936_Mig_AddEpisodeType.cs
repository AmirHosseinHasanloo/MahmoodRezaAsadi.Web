using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class MigAddEpisodeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EpisodeTypeTypeId",
                table: "CourseEpisodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "CourseEpisodes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EpisodeTypes",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EpisodeTypes", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CourseEpisodes_EpisodeTypeTypeId",
                table: "CourseEpisodes",
                column: "EpisodeTypeTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CourseEpisodes_EpisodeTypes_EpisodeTypeTypeId",
                table: "CourseEpisodes",
                column: "EpisodeTypeTypeId",
                principalTable: "EpisodeTypes",
                principalColumn: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourseEpisodes_EpisodeTypes_EpisodeTypeTypeId",
                table: "CourseEpisodes");

            migrationBuilder.DropTable(
                name: "EpisodeTypes");

            migrationBuilder.DropIndex(
                name: "IX_CourseEpisodes_EpisodeTypeTypeId",
                table: "CourseEpisodes");

            migrationBuilder.DropColumn(
                name: "EpisodeTypeTypeId",
                table: "CourseEpisodes");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "CourseEpisodes");
        }
    }
}
