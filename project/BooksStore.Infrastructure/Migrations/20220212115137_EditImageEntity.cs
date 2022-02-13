using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksStore.Infrastructure.Migrations
{
    public partial class EditImageEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Path",
                table: "Imgs");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Imgs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Imgs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Imgs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Imgs");

            migrationBuilder.AddColumn<string>(
                name: "Path",
                table: "Imgs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
