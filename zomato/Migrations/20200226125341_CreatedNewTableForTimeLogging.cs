using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class CreatedNewTableForTimeLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c729c85f-35e5-4726-a843-6deb0ccecc88");

            migrationBuilder.CreateTable(
                name: "TimeLoggings",
                columns: table => new
                {
                    logId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    logCreater = table.Column<string>(nullable: true),
                    logTime = table.Column<long>(nullable: false),
                    issueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeLoggings", x => x.logId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bf2b08a-b7ae-4ba1-b749-34901c86a50d", "3c79f361-b76a-4fee-bc62-6013689bfd5b", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeLoggings");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bf2b08a-b7ae-4ba1-b749-34901c86a50d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c729c85f-35e5-4726-a843-6deb0ccecc88", "032e3d07-c820-4f4f-baa0-9c38ca53cc47", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
