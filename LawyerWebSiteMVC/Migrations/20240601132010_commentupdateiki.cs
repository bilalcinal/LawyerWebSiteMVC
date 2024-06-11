using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerWebSiteMVC.Migrations
{
    public partial class commentupdateiki : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 1, 16, 20, 10, 712, DateTimeKind.Local).AddTicks(6160));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 30, 19, 1, 42, 820, DateTimeKind.Local).AddTicks(8820));
        }
    }
}
