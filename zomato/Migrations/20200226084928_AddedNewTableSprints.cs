using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class AddedNewTableSprints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "87d0668d-687a-4372-9eea-d048532436bc");

            migrationBuilder.AddColumn<int>(
                name: "sprintId1",
                table: "IssueList",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    sprintId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    projectId = table.Column<int>(nullable: false),
                    sprintStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    sprintEndTime = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.sprintId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "902ef230-832a-468f-991d-f2b9b018aff5", "2dc8baad-ec28-4667-8bd4-3648387722b3", "ProjectManager", "PROJECTMANAGER" });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IssueList_Sprints_sprintId1",
                table: "IssueList");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropIndex(
                name: "IX_IssueList_sprintId1",
                table: "IssueList");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "902ef230-832a-468f-991d-f2b9b018aff5");

            migrationBuilder.DropColumn(
                name: "sprintId1",
                table: "IssueList");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "87d0668d-687a-4372-9eea-d048532436bc", "3ae82bb1-b865-4cf3-9eda-bb25322eb10a", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
