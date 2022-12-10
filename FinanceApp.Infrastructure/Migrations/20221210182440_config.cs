using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    public partial class config : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Templates",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BudgetsHistory",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "MonthlyBudget", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "54b87d10-2354-4185-a731-b73ec2d1d9cb", 0, "24219a5d-7f98-4185-94bd-86255ae3f3f8", "User", "guest@mail.com", false, false, null, 0m, "guest@mail.com", "guest_user", "AQAAAAEAACcQAAAAEJNJkKHTGQh65E1FOaP3RLrLsz9ZE/PCzQVAWdZVYLr7e2Jd/PWVvaGEiWa5hrFI2A==", null, false, "0f4a4f85-1ab2-4948-b717-2a4404a19f79", false, "guest_user" });

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Food Payments");

            migrationBuilder.InsertData(
                table: "Templates",
                columns: new[] { "Id", "Cost", "Description", "IsActive", "Name", "Quantity" },
                values: new object[] { 1, 1m, "Tasty waffle", true, "Waffle", 0 });

            migrationBuilder.InsertData(
                table: "BudgetsHistory",
                columns: new[] { "Id", "Description", "EntryDate", "IsActive", "IsPaidFor", "IsSingular", "Name", "PaymentTypeId", "Price", "UserId" },
                values: new object[,]
                {
                    { 1, "Ingredients for the best burgers ever!", new DateTime(2022, 11, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "Burgers", 1, 30.50m, "54b87d10-2354-4185-a731-b73ec2d1d9cb" },
                    { 2, "Sushi with friends", new DateTime(2022, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "Sushi", 1, 43.20m, "54b87d10-2354-4185-a731-b73ec2d1d9cb" },
                    { 3, "Bill", new DateTime(2022, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, true, "Mladya Chinar", 1, 60.00m, "54b87d10-2354-4185-a731-b73ec2d1d9cb" },
                    { 4, "Tub of Protein", new DateTime(2022, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, true, false, "Protein", 1, 40.00m, "54b87d10-2354-4185-a731-b73ec2d1d9cb" }
                });

            migrationBuilder.InsertData(
                table: "CurrentPayments",
                columns: new[] { "Id", "Cost", "Description", "EntryDate", "IsActive", "IsPaidFor", "IsSignular", "Name", "PaymentTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, 16.00m, "Pizza takeout.", new DateTime(2022, 12, 10, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(162), true, false, true, "Pizza", 1, "54b87d10-2354-4185-a731-b73ec2d1d9cb" },
                    { 2, 7.00m, "Bubble tea order.", new DateTime(2022, 12, 8, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(194), true, true, true, "Bubble Tea", 1, "54b87d10-2354-4185-a731-b73ec2d1d9cb" },
                    { 3, 40.00m, "Meal prep for the week.", new DateTime(2022, 12, 5, 20, 24, 39, 799, DateTimeKind.Local).AddTicks(197), true, true, true, "Meal Prep", 1, "54b87d10-2354-4185-a731-b73ec2d1d9cb" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BudgetsHistory",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BudgetsHistory",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BudgetsHistory",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BudgetsHistory",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CurrentPayments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Templates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b87d10-2354-4185-a731-b73ec2d1d9cb");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Templates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "BudgetsHistory",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(400)",
                oldMaxLength: 400);

            migrationBuilder.UpdateData(
                table: "PaymentTypes",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "General");
        }
    }
}
