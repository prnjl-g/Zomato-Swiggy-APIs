using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class UpdatedColumnNameInSprintTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "902ef230-832a-468f-991d-f2b9b018aff5");

            migrationBuilder.RenameColumn(
                name: "sprintEndTime",
                table: "Sprints",
                newName: "sprintEndDate");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c9729b3-7907-401f-a3ba-3295d473b58a", "7a6a74cf-52c2-4f44-aa1c-4ef83f0a9d04", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c9729b3-7907-401f-a3ba-3295d473b58a");

            migrationBuilder.RenameColumn(
                name: "sprintEndDate",
                table: "Sprints",
                newName: "sprintEndTime");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "902ef230-832a-468f-991d-f2b9b018aff5", "2dc8baad-ec28-4667-8bd4-3648387722b3", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
