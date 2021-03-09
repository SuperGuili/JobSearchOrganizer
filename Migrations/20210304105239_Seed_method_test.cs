using Microsoft.EntityFrameworkCore.Migrations;

namespace JobSearchOrganizer.Migrations
{
    public partial class Seed_method_test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Company", "Date", "Expectation", "JobDescription", "JobEmail", "JobLink" },
                values: new object[] { 1, "One", "01/03/2021", 1, "Developer", "email@gmail.com", "www.link.com" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Jobs",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
