using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceApp.Infrastructure.Migrations
{
    public partial class BudgetHistoryDbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EntryDate",
                table: "CurrentPayments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "BudgetsHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPaidFor",
                table: "BudgetsHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSingular",
                table: "BudgetsHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "BudgetsHistory",
                type: "money",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntryDate",
                table: "CurrentPayments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "BudgetsHistory");

            migrationBuilder.DropColumn(
                name: "IsPaidFor",
                table: "BudgetsHistory");

            migrationBuilder.DropColumn(
                name: "IsSingular",
                table: "BudgetsHistory");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "BudgetsHistory");
        }
    }
}
