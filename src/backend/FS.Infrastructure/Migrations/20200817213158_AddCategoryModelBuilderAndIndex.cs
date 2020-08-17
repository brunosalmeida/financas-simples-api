using Microsoft.EntityFrameworkCore.Migrations;

namespace FS.Data.Migrations
{
    public partial class AddCategoryModelBuilderAndIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expenses_Category",
                table: "Expenses",
                column: "Category");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_Category",
                table: "Expenses");
        }
    }
}
