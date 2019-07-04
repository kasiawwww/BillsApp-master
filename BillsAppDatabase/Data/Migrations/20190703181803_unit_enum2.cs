using Microsoft.EntityFrameworkCore.Migrations;

namespace BillsAppDatabase.Data.Migrations
{
    public partial class unit_enum2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "OnlineCardPayment");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Blik");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Transfer");

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 4, "Paypal" },
                    { 5, "UsmiechBabelka" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Item" },
                    { 2, "Kg" },
                    { 3, "Litr" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Units",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Item");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Kg");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Litr");
        }
    }
}
