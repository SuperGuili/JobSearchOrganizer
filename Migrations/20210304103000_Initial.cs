using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(nullable: false),
                    JobDescription = table.Column<string>(nullable: true),
                    JobLink = table.Column<string>(nullable: true),
                    JobEmail = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Expectation = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
