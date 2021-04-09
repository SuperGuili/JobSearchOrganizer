using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class ContactName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContactName",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContactName",
                table: "Jobs");
        }
    }
}
