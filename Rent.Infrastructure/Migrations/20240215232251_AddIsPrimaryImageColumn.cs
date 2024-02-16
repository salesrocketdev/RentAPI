using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rent.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsPrimaryImageColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrimary",
                table: "CarImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrimary",
                table: "CarImages");
        }
    }
}
