using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerWebSiteMVC.Migrations
{
    public partial class commentupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 30, 19, 1, 42, 820, DateTimeKind.Local).AddTicks(8820));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 22, 57, 10, 110, DateTimeKind.Local).AddTicks(1880));
        }
    }
}
