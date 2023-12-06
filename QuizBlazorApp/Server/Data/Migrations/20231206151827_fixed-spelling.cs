using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class fixedspelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseMediaUrl",
                table: "QuizQuestions",
                newName: "UseMedia");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UseMedia",
                table: "QuizQuestions",
                newName: "UseMediaUrl");
        }
    }
}
