using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ThesisOct2023.Migrations
{
    /// <inheritdoc />
    public partial class foodHasAPictureNow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Foods");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Foods",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Foods");

            migrationBuilder.AddColumn<byte[]>(
                name: "Photo",
                table: "Foods",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
