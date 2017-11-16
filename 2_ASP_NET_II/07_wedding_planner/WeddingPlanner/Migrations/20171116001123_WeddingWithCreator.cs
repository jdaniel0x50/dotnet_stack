using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Migrations
{
    public partial class WeddingWithCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Weddings",
                type: "int4",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Weddings",
                type: "int4",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weddings_CreatedByUserUserId",
                table: "Weddings",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weddings_Users_CreatedByUserUserId",
                table: "Weddings",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weddings_Users_CreatedByUserUserId",
                table: "Weddings");

            migrationBuilder.DropIndex(
                name: "IX_Weddings_CreatedByUserUserId",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Weddings");
        }
    }
}
