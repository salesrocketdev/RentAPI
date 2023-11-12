using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRentalProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "Cars",
                newName: "DailyValue");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "SecurityDeposit",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_EmployeeId",
                table: "Rentals",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Employees_EmployeeId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_EmployeeId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "SecurityDeposit",
                table: "Rentals");

            migrationBuilder.RenameColumn(
                name: "DailyValue",
                table: "Cars",
                newName: "Value");
        }
    }
}
