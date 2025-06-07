using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDiscussionTopic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discussion_topics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    Views = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MovieId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discussion_topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_discussion_topics_movies_MovieId",
                        column: x => x.MovieId,
                        principalTable: "movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_discussion_topics_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topic_tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_topic_tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    TopicId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentCommentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comments_comments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "comments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_comments_discussion_topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "discussion_topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comments_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiscussionTopicEntityTopicTagEntity",
                columns: table => new
                {
                    TagsId = table.Column<int>(type: "integer", nullable: false),
                    TopicsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscussionTopicEntityTopicTagEntity", x => new { x.TagsId, x.TopicsId });
                    table.ForeignKey(
                        name: "FK_DiscussionTopicEntityTopicTagEntity_discussion_topics_Topic~",
                        column: x => x.TopicsId,
                        principalTable: "discussion_topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiscussionTopicEntityTopicTagEntity_topic_tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "topic_tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_likes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CommentId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_likes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_comment_likes_comments_CommentId",
                        column: x => x.CommentId,
                        principalTable: "comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_likes_users_UserId",
                        column: x => x.UserId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "topic_tags",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Спойлеры" },
                    { 2, "Фан-теории" },
                    { 3, "Рецензия" },
                    { 4, "Сюжетные дыры" },
                    { 5, "Ошибки и ляпы" },
                    { 6, "Анализ персонажей" },
                    { 7, "Саундтрек" },
                    { 8, "Классика" },
                    { 9, "Новинки" },
                    { 10, "Недооценённое" },
                    { 11, "Операторская работа" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_likes_CommentId",
                table: "comment_likes",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_comment_likes_UserId",
                table: "comment_likes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_ParentCommentId",
                table: "comments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_TopicId",
                table: "comments",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_comments_UserId",
                table: "comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_discussion_topics_MovieId",
                table: "discussion_topics",
                column: "MovieId");

            migrationBuilder.CreateIndex(
                name: "IX_discussion_topics_UserId",
                table: "discussion_topics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DiscussionTopicEntityTopicTagEntity_TopicsId",
                table: "DiscussionTopicEntityTopicTagEntity",
                column: "TopicsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment_likes");

            migrationBuilder.DropTable(
                name: "DiscussionTopicEntityTopicTagEntity");

            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "topic_tags");

            migrationBuilder.DropTable(
                name: "discussion_topics");
        }
    }
}
