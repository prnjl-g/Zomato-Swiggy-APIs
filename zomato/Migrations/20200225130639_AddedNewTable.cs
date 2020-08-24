using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class AddedNewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b6e24b6d-470a-4591-a608-1f864aac7a55");

            migrationBuilder.CreateTable(
                name: "ProjectDetails",
                columns: table => new
                {
                    projectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    projectDescription = table.Column<string>(nullable: true),
                    creatorOfProject = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDetails", x => x.projectId);
                });

            migrationBuilder.CreateTable(
                name: "IssueList",
                columns: table => new
                {
                    issueId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    issueProjectId = table.Column<int>(nullable: false),
                    issueType = table.Column<string>(nullable: true),
                    issueTitle = table.Column<string>(nullable: true),
                    issueDescription = table.Column<string>(nullable: true),
                    issueReporter = table.Column<string>(nullable: true),
                    issueAssignee = table.Column<string>(nullable: true),
                    issueStatus = table.Column<string>(nullable: true),
                    projectId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueList", x => x.issueId);
                    table.ForeignKey(
                        name: "FK_IssueList_ProjectDetails_projectId",
                        column: x => x.projectId,
                        principalTable: "ProjectDetails",
                        principalColumn: "projectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13d47502-87d4-429a-8bde-002e9ff6fc3b", "2b2a5735-5bbf-44e3-9fb2-4f678d8d8ad5", "ProjectManager", "PROJECTMANAGER" });

            migrationBuilder.CreateIndex(
                name: "IX_IssueList_projectId",
                table: "IssueList",
                column: "projectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IssueList");

            migrationBuilder.DropTable(
                name: "ProjectDetails");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13d47502-87d4-429a-8bde-002e9ff6fc3b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b6e24b6d-470a-4591-a608-1f864aac7a55", "0f46980b-ac41-46fd-92e1-f4f00d576678", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
