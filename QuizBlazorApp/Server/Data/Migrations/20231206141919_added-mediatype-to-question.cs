using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizBlazorApp.Server.Data.Migrations
{
    public partial class addedmediatypetoquestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MediaType",
                table: "QuizQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MediaType",
                table: "QuizQuestions");
        }
    }
}
