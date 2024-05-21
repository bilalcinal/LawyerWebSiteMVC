using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerWebSiteMVC.Migrations
{
    public partial class MigSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MyProperty",
                table: "Sliders");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Sliders",
                newName: "Text");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 22, 33, 29, 637, DateTimeKind.Local).AddTicks(5380));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Sliders",
                newName: "Content");

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedDate",
                value: new DateTime(2024, 5, 21, 22, 17, 38, 556, DateTimeKind.Local).AddTicks(5130));
        }
    }
}
