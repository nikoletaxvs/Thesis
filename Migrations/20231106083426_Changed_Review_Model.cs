using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisOct2023.Migrations
{
    /// <inheritdoc />
    public partial class Changed_Review_Model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentEmail",
                table: "Reviews",
                newName: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Reviews",
                newName: "StudentEmail");
        }
    }
}
