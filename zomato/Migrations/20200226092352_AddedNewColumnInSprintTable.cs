using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class AddedNewColumnInSprintTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9729b3-7907-401f-a3ba-3295d473b58a");

            migrationBuilder.AddColumn<string>(
                name: "sprintStatus",
                table: "Sprints",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c7ea694-c6bc-4e96-aaab-2c7e54c8f140", "e5a50ac5-d53c-4ede-bd60-8169e5829938", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c7ea694-c6bc-4e96-aaab-2c7e54c8f140");

            migrationBuilder.DropColumn(
                name: "sprintStatus",
                table: "Sprints");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c9729b3-7907-401f-a3ba-3295d473b58a", "7a6a74cf-52c2-4f44-aa1c-4ef83f0a9d04", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
