using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisOct2023.Migrations
{
    /// <inheritdoc />
    public partial class commentsareinreviewquestionsnow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ReviewQuestions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ReviewQuestions");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
