using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    public partial class config2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afbac7db-1e9b-478a-b089-c84b7ae50c98", "AQAAAAEAACcQAAAAEHWmQdGl6EyFlS2ClMhftUFEL3uVIf2MPSCLpWcXngVN3pzOlu41ossFth1JxnS8jg==", "acc5fc53-cb5c-4df0-82b2-b7b520872fa9" });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 10, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2327), 1 });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 8, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2359), 1 });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 5, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2362), 1 });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.InsertData(
                table: "PaymentTypes",
                columns: new[] { "Id", "IsActive", "Name" },
                values: new object[] { 2, true, "Car Payments" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "24219a5d-7f98-4185-94bd-86255ae3f3f8", "AQAAAAEAACcQAAAAEJNJkKHTGQh65E1FOaP3RLrLsz9ZE/PCzQVAWdZVYLr7e2Jd/PWVvaGEiWa5hrFI2A==", "0f4a4f85-1ab2-4948-b717-2a4404a19f79" });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 10, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(162), 1 });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 8, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(194), 1 });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EntryDate", "PaymentTypeId" },
                values: new object[] { new DateTime(2022, 12, 5, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(197), 1 });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);
        }
    }
}
