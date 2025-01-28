using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace biblioteca.Migrations
{
    /// <inheritdoc />
    public partial class gs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Books",
                type: "longtext",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Synopsis",
                table: "Books",
                type: "longtext",
                nullable: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Synopsis",
                table: "Books");
        }
    }
}
