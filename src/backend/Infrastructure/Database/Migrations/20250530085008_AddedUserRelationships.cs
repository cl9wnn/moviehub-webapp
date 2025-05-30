using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorEntityUserEntity",
                columns: table => new
                {
                    FavoriteActorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorEntityUserEntity", x => new { x.FavoriteActorsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_ActorEntityUserEntity_actors_FavoriteActorsId",
                        column: x => x.FavoriteActorsId,
                        principalTable: "actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorEntityUserEntity_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenreEntityUserEntity",
                columns: table => new
                {
                    PreferredGenresId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreEntityUserEntity", x => new { x.PreferredGenresId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_GenreEntityUserEntity_genres_PreferredGenresId",
                        column: x => x.PreferredGenresId,
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreEntityUserEntity_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntityUserEntity",
                columns: table => new
                {
                    UsersId = table.Column<Guid>(type: "uuid", nullable: false),
                    WatchListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntityUserEntity", x => new { x.UsersId, x.WatchListId });
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity_movies_WatchListId",
                        column: x => x.WatchListId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieEntityUserEntity_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorEntityUserEntity_UsersId",
                table: "ActorEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreEntityUserEntity_UsersId",
                table: "GenreEntityUserEntity",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntityUserEntity_WatchListId",
                table: "MovieEntityUserEntity",
                column: "WatchListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorEntityUserEntity");

            migrationBuilder.DropTable(
                name: "GenreEntityUserEntity");

            migrationBuilder.DropTable(
                name: "MovieEntityUserEntity");
        }
    }
}
