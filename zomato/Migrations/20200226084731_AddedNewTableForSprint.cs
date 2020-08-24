using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class AddedNewTableForSprint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddfa4c07-f042-4a21-86c2-bb9eff6e2f5a");

            migrationBuilder.AddColumn<string>(
                name: "sprintId",
                table: "IssueList",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87d0668d-687a-4372-9eea-d048532436bc", "3ae82bb1-b865-4cf3-9eda-bb25322eb10a", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d0668d-687a-4372-9eea-d048532436bc");

            migrationBuilder.DropColumn(
                name: "sprintId",
                table: "IssueList");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ddfa4c07-f042-4a21-86c2-bb9eff6e2f5a", "d99c1579-1c52-408f-b6f1-32753ec3511d", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
