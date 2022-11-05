using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogProject.DAL.Migrations
{
    public partial class V2readcntr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4527cf02-5940-4714-be93-936b906801a5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b4b05be1-abd2-43ec-a615-3b63ae4e4cf6");

            migrationBuilder.AddColumn<int>(
                name: "ReadCounter",
                table: "Articles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "40ecfa2e-4e22-4c31-868a-72b0eabfacb8", "cdb40b16-edda-4ca4-81f3-0dee95d7d890", "Member", "MEMBER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c35aa274-2bfe-4077-92dc-871233265733", "43ba42b9-993c-4b3e-a9ff-68b312ebac73", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40ecfa2e-4e22-4c31-868a-72b0eabfacb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c35aa274-2bfe-4077-92dc-871233265733");

            migrationBuilder.DropColumn(
                name: "ReadCounter",
                table: "Articles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4527cf02-5940-4714-be93-936b906801a5", "91fb9abf-50a8-4275-9916-3fc0e62f822c", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b4b05be1-abd2-43ec-a615-3b63ae4e4cf6", "f5021903-fc9c-488a-abe7-8d11701a243e", "Member", "MEMBER" });
        }
    }
}
