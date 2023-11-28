using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisOct2023.Migrations
{
    /// <inheritdoc />
    public partial class SumFieldOnReviewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SumScore",
                table: "Reviews",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SumScore",
                table: "Reviews");
        }
    }
}
