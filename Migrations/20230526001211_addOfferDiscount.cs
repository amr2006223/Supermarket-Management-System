using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Supermarket_Managment_System.Migrations
{
    /// <inheritdoc />
    public partial class addOfferDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Discount",
                table: "offers",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "offers");
        }
    }
}
