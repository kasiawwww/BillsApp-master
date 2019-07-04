using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BillsAppDatabase.Data.Migrations
{
    public partial class unit_enum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Units");

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
        }
    }
}
