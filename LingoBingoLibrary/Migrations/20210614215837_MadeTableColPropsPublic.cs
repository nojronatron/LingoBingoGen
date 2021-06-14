using Microsoft.EntityFrameworkCore.Migrations;

namespace LingoBingoLibrary.Migrations
{
    public partial class MadeTableColPropsPublic : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Word",
                table: "LingoWords",
                type: "varchar(45)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "LingoCategories",
                type: "varchar(45)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Word",
                table: "LingoWords");

            migrationBuilder.DropColumn(
                name: "Category",
                table: "LingoCategories");
        }
    }
}
