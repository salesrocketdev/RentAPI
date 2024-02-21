using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdjustCarImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarImages");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CarImages");

            migrationBuilder.RenameColumn(
                name: "Base64",
                table: "CarImages",
                newName: "Link");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Link",
                table: "CarImages",
                newName: "Base64");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CarImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CarImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CarImages",
                type: "datetime2",
                nullable: true);
        }
    }
}
