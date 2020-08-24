using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class Updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueList_Sprints_sprintId1",
                table: "IssueList");

            migrationBuilder.DropIndex(
                name: "IX_IssueList_sprintId1",
                table: "IssueList");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1c7ea694-c6bc-4e96-aaab-2c7e54c8f140");

            migrationBuilder.DropColumn(
                name: "sprintId1",
                table: "IssueList");

            migrationBuilder.AlterColumn<int>(
                name: "sprintId",
                table: "IssueList",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "issueSprintId",
                table: "IssueList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "869097c6-5ba8-4a64-9245-0688edd9dc64", "ad0f3149-bf82-44b7-aadc-7b31c1cb49f6", "ProjectManager", "PROJECTMANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_IssueList_sprintId",
                table: "IssueList",
                column: "sprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueList_Sprints_sprintId",
                table: "IssueList",
                column: "sprintId",
                principalTable: "Sprints",
                principalColumn: "sprintId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueList_Sprints_sprintId",
                table: "IssueList");

            migrationBuilder.DropIndex(
                name: "IX_IssueList_sprintId",
                table: "IssueList");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "869097c6-5ba8-4a64-9245-0688edd9dc64");

            migrationBuilder.DropColumn(
                name: "issueSprintId",
                table: "IssueList");

            migrationBuilder.AlterColumn<string>(
                name: "sprintId",
                table: "IssueList",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "sprintId1",
                table: "IssueList",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1c7ea694-c6bc-4e96-aaab-2c7e54c8f140", "e5a50ac5-d53c-4ede-bd60-8169e5829938", "ProjectManager", "PROJECTMANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_IssueList_sprintId1",
                table: "IssueList",
                column: "sprintId1");

            migrationBuilder.AddForeignKey(
                name: "FK_IssueList_Sprints_sprintId1",
                table: "IssueList",
                column: "sprintId1",
                principalTable: "Sprints",
                principalColumn: "sprintId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
