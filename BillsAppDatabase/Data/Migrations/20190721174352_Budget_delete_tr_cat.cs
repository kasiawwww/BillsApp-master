using Microsoft.EntityFrameworkCore.Migrations;

namespace BillsAppDatabase.Data.Migrations
{
    public partial class Budget_delete_tr_cat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Budgets_TransactionCategories_TransactionCategoryId",
                table: "Budgets");

            migrationBuilder.DropIndex(
                name: "IX_Budgets_TransactionCategoryId",
                table: "Budgets");

            migrationBuilder.DropColumn(
                name: "Unit",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "TransactionCategoryId",
                table: "Budgets");

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_UnitId",
                table: "Products",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UnitId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "Products",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TransactionCategoryId",
                table: "Budgets",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_TransactionCategoryId",
                table: "Budgets",
                column: "TransactionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Budgets_TransactionCategories_TransactionCategoryId",
                table: "Budgets",
                column: "TransactionCategoryId",
                principalTable: "TransactionCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
