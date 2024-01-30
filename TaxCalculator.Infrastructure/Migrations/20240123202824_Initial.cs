using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaxCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProgressiveTaxBrackets",
                columns: table => new
                {
                    From = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    To = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RatePercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgressiveTaxBrackets");
        }
    }
}
