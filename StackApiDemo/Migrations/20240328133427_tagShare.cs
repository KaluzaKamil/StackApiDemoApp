using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class tagShare : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Share",
                table: "Tags",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Share",
                table: "Tags");
        }
    }
}
