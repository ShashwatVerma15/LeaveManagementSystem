using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class added_leave_allocation_relations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1fa0db8-96fa-4f6d-aa07-799975b6a840", "AQAAAAIAAYagAAAAEJL2oqEI4CDCdx7IYv4QtNyea4yOI24KAp4GK3P0hKo3WQLsAuMWRIBRlivDFo81ew==", "e4b5619d-8d69-4583-ac23-d637c7e66367" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "07af0ac6-a3da-409a-9f36-59d24292fa95", "AQAAAAIAAYagAAAAEEmz4XvSkmGl5bqZRdMSJH9dOI9/sCxHrlKyiZsjQCLo3eSgmiovyO2OB9UKnxRCvQ==", "b25b0108-17b1-410a-97cc-6fd596adbe37" });
        }
    }
}
