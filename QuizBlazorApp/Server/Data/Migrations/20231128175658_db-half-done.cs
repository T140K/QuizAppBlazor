using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class dbhalfdone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuizGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatorName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKQuizGameId = table.Column<int>(type: "int", nullable: false),
                    QuestionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsTimed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestions_QuizGames_FKQuizGameId",
                        column: x => x.FKQuizGameId,
                        principalTable: "QuizGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKQuestionId = table.Column<int>(type: "int", nullable: false),
                    AnswerTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionAnswers_QuizQuestions_FKQuestionId",
                        column: x => x.FKQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuizQuestionTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKQuestionId = table.Column<int>(type: "int", nullable: false),
                    TimeLimit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuizQuestionTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuizQuestionTimes_QuizQuestions_FKQuestionId",
                        column: x => x.FKQuestionId,
                        principalTable: "QuizQuestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionAnswers_FKQuestionId",
                table: "QuizQuestionAnswers",
                column: "FKQuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestions_FKQuizGameId",
                table: "QuizQuestions",
                column: "FKQuizGameId");

            migrationBuilder.CreateIndex(
                name: "IX_QuizQuestionTimes_FKQuestionId",
                table: "QuizQuestionTimes",
                column: "FKQuestionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizQuestionAnswers");

            migrationBuilder.DropTable(
                name: "QuizQuestionTimes");

            migrationBuilder.DropTable(
                name: "QuizQuestions");

            migrationBuilder.DropTable(
                name: "QuizGames");
        }
    }
}
