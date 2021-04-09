using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Added_PartTime_Apprentice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprentice",
                table: "Jobs",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartTime",
                table: "Jobs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprentice",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "IsPartTime",
                table: "Jobs");
        }
    }
}
