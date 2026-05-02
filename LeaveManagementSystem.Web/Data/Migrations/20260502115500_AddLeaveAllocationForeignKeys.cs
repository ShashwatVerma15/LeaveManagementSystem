using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLeaveAllocationForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "59ba3955-366c-4608-9505-e403dd0f9698", "AQAAAAIAAYagAAAAEP49hYhESump5VDfm6M7OSzD9h76/itTFrT05qE+C1MsYYf+gfT8Lgru/uf1WsMTiA==", "80592c31-8c87-4c38-963d-82c2e36b6d6b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b33f8105-61d2-4cdc-816e-3d96573cbd34",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a1fa0db8-96fa-4f6d-aa07-799975b6a840", "AQAAAAIAAYagAAAAEJL2oqEI4CDCdx7IYv4QtNyea4yOI24KAp4GK3P0hKo3WQLsAuMWRIBRlivDFo81ew==", "e4b5619d-8d69-4583-ac23-d637c7e66367" });
        }
    }
}
