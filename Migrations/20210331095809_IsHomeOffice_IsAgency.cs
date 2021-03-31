using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class IsHomeOffice_IsAgency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HomeOffice",
                table: "Jobs");

            migrationBuilder.AddColumn<bool>(
                name: "IsAgency",
                table: "Jobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHomeOffice",
                table: "Jobs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAgency",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsHomeOffice",
                table: "Jobs");

            migrationBuilder.AddColumn<bool>(
                name: "HomeOffice",
                table: "Jobs",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
