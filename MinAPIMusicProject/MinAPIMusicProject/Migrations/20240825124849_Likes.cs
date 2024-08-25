using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MinAPIMusicProject.Migrations
{
    /// <inheritdoc />
    public partial class Likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikedTracksIds",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LikesCount",
                table: "Tracks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedTracksIds",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LikesCount",
                table: "Tracks");
        }
    }
}
