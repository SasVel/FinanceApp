using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    public partial class currencyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                columns: new[] { "ConcurrencyStamp", "Currency", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d476004f-7b5e-4bf7-9961-2d1c1f66ad0f", "BGN", "AQAAAAEAACcQAAAAEAP6a7WZtIeR2te+dOOzPP6ItMY6ShGoceMePP5HnWd181RWA0/IMSdD8LGYbU/J4w==", "37a730f2-3244-4e54-bf90-c9abac199c37" });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryDate",
                value: new DateTime(2022, 12, 13, 22, 39, 58, 913, DateTimeKind.Local).AddTicks(7800));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryDate",
                value: new DateTime(2022, 12, 11, 22, 39, 58, 913, DateTimeKind.Local).AddTicks(7818));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryDate",
                value: new DateTime(2022, 12, 8, 22, 39, 58, 913, DateTimeKind.Local).AddTicks(7825));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Currency",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b87d10-2354-4185-a731-b73ec2d1d9cb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "27402bc2-c755-46c5-a390-60beaa274ec4", "AQAAAAEAACcQAAAAEOKFADSzZs/O6ClGgJx1gnavE64ftwuHj6o4AE3oTheKfNMQSwi5ekTIjmu2/WJi8Q==", "fd395e01-d231-4c65-a10a-b4331d5adb4f" });

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 1,
                column: "EntryDate",
                value: new DateTime(2022, 12, 13, 1, 34, 50, 300, DateTimeKind.Local).AddTicks(9123));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryDate",
                value: new DateTime(2022, 12, 11, 1, 34, 50, 300, DateTimeKind.Local).AddTicks(9136));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryDate",
                value: new DateTime(2022, 12, 8, 1, 34, 50, 300, DateTimeKind.Local).AddTicks(9145));
        }
    }
}
