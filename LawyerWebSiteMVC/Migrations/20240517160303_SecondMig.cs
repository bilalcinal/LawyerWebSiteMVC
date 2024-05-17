using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerWebSiteMVC.Migrations
{
    public partial class SecondMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "Sliders",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "Letters",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "Comments",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "Articles",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "İsDeleted",
                table: "ArticlePhotos",
                newName: "IsDeleted");

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "CreatedDate", "Email", "FullName", "IsDeleted", "Password", "Role" },
                values: new object[] { 1, new DateTime(2024, 5, 17, 19, 3, 2, 972, DateTimeKind.Local).AddTicks(7789), "admin@admin.com", "Admin", false, "Admin123", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Sliders",
                newName: "İsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Letters",
                newName: "İsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Comments",
                newName: "İsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Categories",
                newName: "İsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Articles",
                newName: "İsDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ArticlePhotos",
                newName: "İsDeleted");
        }
    }
}
