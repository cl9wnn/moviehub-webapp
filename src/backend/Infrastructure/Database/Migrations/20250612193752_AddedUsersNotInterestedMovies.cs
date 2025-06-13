using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUsersNotInterestedMovies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity_movies_WatchListId",
                table: "MovieEntityUserEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieEntityUserEntity_users_UsersId",
                table: "MovieEntityUserEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieEntityUserEntity",
                table: "MovieEntityUserEntity");

            migrationBuilder.RenameTable(
                name: "MovieEntityUserEntity",
                newName: "WatchList");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "WatchList",
                newName: "UsersWatchListId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieEntityUserEntity_WatchListId",
                table: "WatchList",
                newName: "IX_WatchList_WatchListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList",
                columns: new[] { "UsersWatchListId", "WatchListId" });

            migrationBuilder.CreateTable(
                name: "NotInterestedMovies",
                columns: table => new
                {
                    NotInterestedMoviesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersNotInterestedId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotInterestedMovies", x => new { x.NotInterestedMoviesId, x.UsersNotInterestedId });
                    table.ForeignKey(
                        name: "FK_NotInterestedMovies_movies_NotInterestedMoviesId",
                        column: x => x.NotInterestedMoviesId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotInterestedMovies_users_UsersNotInterestedId",
                        column: x => x.UsersNotInterestedId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotInterestedMovies_UsersNotInterestedId",
                table: "NotInterestedMovies",
                column: "UsersNotInterestedId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_movies_WatchListId",
                table: "WatchList",
                column: "WatchListId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchList_users_UsersWatchListId",
                table: "WatchList",
                column: "UsersWatchListId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_movies_WatchListId",
                table: "WatchList");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchList_users_UsersWatchListId",
                table: "WatchList");

            migrationBuilder.DropTable(
                name: "NotInterestedMovies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchList",
                table: "WatchList");

            migrationBuilder.RenameTable(
                name: "WatchList",
                newName: "MovieEntityUserEntity");

            migrationBuilder.RenameColumn(
                name: "UsersWatchListId",
                table: "MovieEntityUserEntity",
                newName: "UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchList_WatchListId",
                table: "MovieEntityUserEntity",
                newName: "IX_MovieEntityUserEntity_WatchListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieEntityUserEntity",
                table: "MovieEntityUserEntity",
                columns: new[] { "UsersId", "WatchListId" });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity_movies_WatchListId",
                table: "MovieEntityUserEntity",
                column: "WatchListId",
                principalTable: "movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieEntityUserEntity_users_UsersId",
                table: "MovieEntityUserEntity",
                column: "UsersId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
