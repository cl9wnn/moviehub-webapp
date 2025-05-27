using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "movies",
                newName: "RatingSum");

            migrationBuilder.AddColumn<int>(
                name: "RatingCount",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RatingCount",
                table: "movies");

            migrationBuilder.RenameColumn(
                name: "RatingSum",
                table: "movies",
                newName: "UserRating");
        }
    }
}
