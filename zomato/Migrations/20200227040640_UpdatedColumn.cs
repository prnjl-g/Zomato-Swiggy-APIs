using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class UpdatedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5bf2b08a-b7ae-4ba1-b749-34901c86a50d");

            migrationBuilder.AlterColumn<string>(
                name: "issueStatus",
                table: "IssueList",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "239c5447-af76-4fbc-86ab-85e29255d497", "e6af23b0-8c30-4e78-9536-b8e1441cdef7", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "239c5447-af76-4fbc-86ab-85e29255d497");

            migrationBuilder.AlterColumn<string>(
                name: "issueStatus",
                table: "IssueList",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5bf2b08a-b7ae-4ba1-b749-34901c86a50d", "3c79f361-b76a-4fee-bc62-6013689bfd5b", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
