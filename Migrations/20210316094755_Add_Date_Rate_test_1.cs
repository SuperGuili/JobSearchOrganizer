using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Add_Date_Rate_test_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobEmail",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "Expectation",
                table: "Jobs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<decimal>(
                name: "AnnualRate",
                table: "Jobs",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "AppliedDate",
                table: "Jobs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ContactEmail",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobPosition",
                table: "Jobs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobStatus",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnnualRate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AppliedDate",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "ContactEmail",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobPosition",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobStatus",
                table: "Jobs");

            migrationBuilder.AlterColumn<int>(
                name: "Expectation",
                table: "Jobs",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobEmail",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
