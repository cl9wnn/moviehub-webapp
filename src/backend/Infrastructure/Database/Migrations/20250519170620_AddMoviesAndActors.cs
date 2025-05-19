using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMoviesAndActors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "actors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Biography = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PhotoUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_actors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movie_directors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_directors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movie_writers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_writers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "movies",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    DurationAtMinutes = table.Column<int>(type: "integer", nullable: false),
                    AgeRating = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    UserRating = table.Column<double>(type: "double precision", nullable: false),
                    PosterUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreEntityMovieEntity",
                columns: table => new
                {
                    GenresId = table.Column<int>(type: "integer", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreEntityMovieEntity", x => new { x.GenresId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_GenreEntityMovieEntity_genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreEntityMovieEntity_movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "movie_actors",
                columns: table => new
                {
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: false),
                    CharacterName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_actors", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_movie_actors_actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_actors_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieDirectorEntityMovieEntity",
                columns: table => new
                {
                    DirectorsId = table.Column<Guid>(type: "uuid", nullable: false),
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieDirectorEntityMovieEntity", x => new { x.DirectorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_MovieDirectorEntityMovieEntity_movie_directors_DirectorsId",
                        column: x => x.DirectorsId,
                        principalTable: "movie_directors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieDirectorEntityMovieEntity_movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieEntityMovieWriterEntity",
                columns: table => new
                {
                    MoviesId = table.Column<Guid>(type: "uuid", nullable: false),
                    WritersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieEntityMovieWriterEntity", x => new { x.MoviesId, x.WritersId });
                    table.ForeignKey(
                        name: "FK_MovieEntityMovieWriterEntity_movie_writers_WritersId",
                        column: x => x.WritersId,
                        principalTable: "movie_writers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieEntityMovieWriterEntity_movies_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageUrl = table.Column<string>(type: "text", nullable: false),
                    PhotoType = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: false),
                    ActorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_photos_actors_ActorId",
                        column: x => x.ActorId,
                        principalTable: "actors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_photos_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "genres",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Action" },
                    { 2, "Adventure" },
                    { 3, "Comedy" },
                    { 4, "Drama" },
                    { 5, "Horror" },
                    { 6, "Sci-Fi" },
                    { 7, "Fantasy" },
                    { 8, "Romance" },
                    { 9, "Thriller" },
                    { 10, "Crime" },
                    { 11, "Mystery" },
                    { 12, "Animation" },
                    { 13, "Documentary" },
                    { 14, "Western" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GenreEntityMovieEntity_MoviesId",
                table: "GenreEntityMovieEntity",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_movie_actors_ActorId",
                table: "movie_actors",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieDirectorEntityMovieEntity_MoviesId",
                table: "MovieDirectorEntityMovieEntity",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieEntityMovieWriterEntity_WritersId",
                table: "MovieEntityMovieWriterEntity",
                column: "WritersId");

            migrationBuilder.CreateIndex(
                name: "IX_photos_ActorId",
                table: "photos",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_photos_MovieId",
                table: "photos",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenreEntityMovieEntity");

            migrationBuilder.DropTable(
                name: "movie_actors");

            migrationBuilder.DropTable(
                name: "MovieDirectorEntityMovieEntity");

            migrationBuilder.DropTable(
                name: "MovieEntityMovieWriterEntity");

            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "movie_directors");

            migrationBuilder.DropTable(
                name: "movie_writers");

            migrationBuilder.DropTable(
                name: "actors");

            migrationBuilder.DropTable(
                name: "movies");
        }
    }
}
