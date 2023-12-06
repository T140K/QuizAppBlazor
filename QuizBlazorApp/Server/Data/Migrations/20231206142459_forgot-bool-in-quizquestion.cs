using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class forgotboolinquizquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseMediaUrl",
                table: "QuizQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UseMediaUrl",
                table: "QuizQuestions");
        }
    }
}
