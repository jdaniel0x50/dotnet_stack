using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Migrations
{
    public partial class YourMigrationName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Weddings");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Weddings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wedder1",
                table: "Weddings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wedder2",
                table: "Weddings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "WeddingDate",
                table: "Weddings",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "Wedder1",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "Wedder2",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "WeddingDate",
                table: "Weddings");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Weddings",
                nullable: true);
        }
    }
}
