using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Bonus_Close_Feedback_HomeOffice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Bonus",
                table: "Jobs",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HomeOffice",
                table: "Jobs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bonus",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "HomeOffice",
                table: "Jobs");
        }
    }
}
