using Microsoft.EntityFrameworkCore.Migrations;

namespace MengYu.BudgetTracker.Infrastructure.Migrations
{
    public partial class UserExpenditureRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Expenditures_UserId",
                table: "Expenditures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_Users_UserId",
                table: "Expenditures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_Users_UserId",
                table: "Expenditures");

            migrationBuilder.DropIndex(
                name: "IX_Expenditures_UserId",
                table: "Expenditures");
        }
    }
}
