using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixFieldName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Tests",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Questions",
                newName: "Title");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Tests",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Questions",
                newName: "Tittle");
        }
    }
}
