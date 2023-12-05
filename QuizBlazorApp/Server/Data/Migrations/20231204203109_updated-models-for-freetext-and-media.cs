using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class updatedmodelsforfreetextandmedia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFreeTextAnswer",
                table: "QuizQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MediaUrl",
                table: "QuizQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFreeTextAnswer",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "MediaUrl",
                table: "QuizQuestions");
        }
    }
}
