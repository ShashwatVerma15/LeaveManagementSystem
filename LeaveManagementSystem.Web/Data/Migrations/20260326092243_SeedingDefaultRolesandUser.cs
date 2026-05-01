using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRolesandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "25a693d9-0574-4b12-bfec-12e015fe453e", null, "Administrator", "ADMINISTRATOR" },
                    { "72ab4a98-c2d3-4a97-b3e7-73d12f5ab1c1", null, "Employee", "Employee" },
                    { "c8a7359c-f6af-4355-9310-3f394e8f37f7", null, "Supervisor", "SUPERVISOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b33f8105-61d2-4cdc-816e-3d96573cbd34", 0, "6aab6389-3d79-4aad-bb2b-ad2f853b1a5d", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEANXddM0hZXt85qTme0H8m7q5S9FEvCFsh5dRq1DP9/3MnC88tRXBrjDSv5+Qi60OQ==", null, false, "b377bb4f-97a3-4555-a7d4-2ba6dcf1f115", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "25a693d9-0574-4b12-bfec-12e015fe453e", "b33f8105-61d2-4cdc-816e-3d96573cbd34" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "72ab4a98-c2d3-4a97-b3e7-73d12f5ab1c1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8a7359c-f6af-4355-9310-3f394e8f37f7");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "25a693d9-0574-4b12-bfec-12e015fe453e", "b33f8105-61d2-4cdc-816e-3d96573cbd34" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "25a693d9-0574-4b12-bfec-12e015fe453e");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34");
        }
    }
}
