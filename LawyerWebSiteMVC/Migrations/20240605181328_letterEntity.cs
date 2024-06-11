using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerWebSiteMVC.Migrations
{
    public partial class letterEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Letters");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 5, 21, 13, 28, 298, DateTimeKind.Local).AddTicks(4810));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Letters",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 6, 4, 20, 5, 18, 183, DateTimeKind.Local).AddTicks(2620));
        }
    }
}
