using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentAPI.Migrations
{
    /// <inheritdoc />
    public partial class AdjustingDocumentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Document_Customers_CustomerId",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                table: "Document");

            migrationBuilder.RenameTable(
                name: "Document",
                newName: "Documents");

            migrationBuilder.RenameColumn(
                name: "Rg",
                table: "Documents",
                newName: "RG");

            migrationBuilder.RenameColumn(
                name: "CnhNumber",
                table: "Documents",
                newName: "DriverLicenseNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Document_CustomerId",
                table: "Documents",
                newName: "IX_Documents_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Customers_CustomerId",
                table: "Documents",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Customers_CustomerId",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                table: "Documents");

            migrationBuilder.RenameTable(
                name: "Documents",
                newName: "Document");

            migrationBuilder.RenameColumn(
                name: "RG",
                table: "Document",
                newName: "Rg");

            migrationBuilder.RenameColumn(
                name: "DriverLicenseNumber",
                table: "Document",
                newName: "CnhNumber");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_CustomerId",
                table: "Document",
                newName: "IX_Document_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                table: "Document",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Document_Customers_CustomerId",
                table: "Document",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
