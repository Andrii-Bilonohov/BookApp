using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookApp.Storage.Migrations
{
    /// <inheritdoc />
    public partial class changeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "AuthorBooks",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AuthorBooks");
        }
    }
}
