using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class CreatedNewTableTimeLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e28aa3fd-19d9-4e03-bb5d-a062251c46c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c729c85f-35e5-4726-a843-6deb0ccecc88", "032e3d07-c820-4f4f-baa0-9c38ca53cc47", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c729c85f-35e5-4726-a843-6deb0ccecc88");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e28aa3fd-19d9-4e03-bb5d-a062251c46c2", "54d4851b-8c00-4741-b14d-52973abf6225", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
