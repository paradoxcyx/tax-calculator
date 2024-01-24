using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PayspaceTax.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetPrimaryKeyForTaxBracketsTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProgressiveTaxBrackets",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProgressiveTaxBrackets",
                table: "ProgressiveTaxBrackets",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProgressiveTaxBrackets",
                table: "ProgressiveTaxBrackets");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProgressiveTaxBrackets");
        }
    }
}
