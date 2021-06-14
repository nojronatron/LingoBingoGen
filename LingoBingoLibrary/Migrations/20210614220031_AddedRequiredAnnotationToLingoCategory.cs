using Microsoft.EntityFrameworkCore.Migrations;

namespace LingoBingoLibrary.Migrations
{
    public partial class AddedRequiredAnnotationToLingoCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "LingoCategories",
                type: "varchar(45)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "LingoCategories",
                type: "varchar(45)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(45)");
        }
    }
}
