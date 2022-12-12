using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    public partial class UserIdAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Templates",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "PaymentTypes",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "54b87d10-2354-4185-a731-b73ec2d1d9cb");

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 2,
                column: "UserId",
                value: "54b87d10-2354-4185-a731-b73ec2d1d9cb");

            migrationBuilder.UpdateData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1,
                column: "UserId",
                value: "54b87d10-2354-4185-a731-b73ec2d1d9cb");

            migrationBuilder.CreateIndex(
                name: "IX_Templates_UserId",
                table: "Templates",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_UserId",
                table: "PaymentTypes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTypes_AspNetUsers_UserId",
                table: "PaymentTypes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Templates_AspNetUsers_UserId",
                table: "Templates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTypes_AspNetUsers_UserId",
                table: "PaymentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Templates_AspNetUsers_UserId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_Templates_UserId",
                table: "Templates");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTypes_UserId",
                table: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "PaymentTypes");

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
                column: "EntryDate",
                value: new DateTime(2022, 12, 10, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2327));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2,
                column: "EntryDate",
                value: new DateTime(2022, 12, 8, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2359));

            migrationBuilder.UpdateData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3,
                column: "EntryDate",
                value: new DateTime(2022, 12, 5, 20, 49, 18, 799, DateTimeKind.Local).AddTicks(2362));
        }
    }
}
