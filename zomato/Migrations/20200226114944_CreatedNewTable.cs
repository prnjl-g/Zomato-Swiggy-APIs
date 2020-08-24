using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class CreatedNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "869097c6-5ba8-4a64-9245-0688edd9dc64");

            migrationBuilder.CreateTable(
                name: "Labels",
                columns: table => new
                {
                    labelId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    label = table.Column<string>(nullable: true),
                    issueId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Labels", x => x.labelId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e28aa3fd-19d9-4e03-bb5d-a062251c46c2", "54d4851b-8c00-4741-b14d-52973abf6225", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Labels");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e28aa3fd-19d9-4e03-bb5d-a062251c46c2");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "869097c6-5ba8-4a64-9245-0688edd9dc64", "ad0f3149-bf82-44b7-aadc-7b31c1cb49f6", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
