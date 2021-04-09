using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Added_JobDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Jobs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Jobs");
        }
    }
}
