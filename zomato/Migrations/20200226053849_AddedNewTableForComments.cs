using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace graphql_create.Migrations
{
    public partial class AddedNewTableForComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13d47502-87d4-429a-8bde-002e9ff6fc3b");

            migrationBuilder.CreateTable(
                name: "CommentsOnIssues",
                columns: table => new
                {
                    commentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    issueId = table.Column<int>(nullable: false),
                    comment = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsOnIssues", x => x.commentId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ddfa4c07-f042-4a21-86c2-bb9eff6e2f5a", "d99c1579-1c52-408f-b6f1-32753ec3511d", "ProjectManager", "PROJECTMANAGER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsOnIssues");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ddfa4c07-f042-4a21-86c2-bb9eff6e2f5a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "13d47502-87d4-429a-8bde-002e9ff6fc3b", "2b2a5735-5bbf-44e3-9fb2-4f678d8d8ad5", "ProjectManager", "PROJECTMANAGER" });
        }
    }
}
