using Microsoft.EntityFrameworkCore.Migrations;

namespace LingoBingoLibrary.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LingoCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LingoCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LingoWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LingoCategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LingoWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LingoWords_LingoCategories_LingoCategoryId",
                        column: x => x.LingoCategoryId,
                        principalTable: "LingoCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LingoWords_LingoCategoryId",
                table: "LingoWords",
                column: "LingoCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LingoWords");

            migrationBuilder.DropTable(
                name: "LingoCategories");
        }
    }
}
