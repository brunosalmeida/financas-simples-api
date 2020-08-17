using Microsoft.EntityFrameworkCore.Migrations;

namespace FS.Data.Migrations
{
    public partial class AddExpenseCreatedOnIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expenses_CreatedOn",
                table: "Expenses",
                column: "CreatedOn");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Expenses_CreatedOn",
                table: "Expenses");
        }
    }
}
