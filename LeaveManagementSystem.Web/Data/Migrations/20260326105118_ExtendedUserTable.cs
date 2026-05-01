using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8abb5f56-1977-4d86-8978-759be033e29b", new DateOnly(2000, 1, 30), "Default", "Admin", "AQAAAAIAAYagAAAAEHDl99GSc8xVAFANm0EbCwWg+umVLQQuBlBos3NQiscJ31jtDKQ4/GJBembc/hK/fg==", "04c7e381-5432-409f-8f97-d3c4a1634df4" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6aab6389-3d79-4aad-bb2b-ad2f853b1a5d", "AQAAAAIAAYagAAAAEANXddM0hZXt85qTme0H8m7q5S9FEvCFsh5dRq1DP9/3MnC88tRXBrjDSv5+Qi60OQ==", "b377bb4f-97a3-4555-a7d4-2ba6dcf1f115" });
        }
    }
}
