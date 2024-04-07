using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ReAddDocumentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriverLicenseNumber",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RG",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverLicenseNumber",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "RG",
                table: "Documents");
        }
    }
}
