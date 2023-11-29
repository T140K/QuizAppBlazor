using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class removedquestionTimetableaddedtimetoquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuizQuestionTimes");

            migrationBuilder.AddColumn<int>(
                name: "TimeLimit",
                table: "QuizQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeLimit",
                table: "QuizQuestions");

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
                name: "IX_QuizQuestionTimes_FKQuestionId",
                table: "QuizQuestionTimes",
                column: "FKQuestionId",
                unique: true);
        }
    }
}
