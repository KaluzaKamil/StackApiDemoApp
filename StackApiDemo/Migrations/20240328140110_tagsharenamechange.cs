using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class tagsharenamechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Share",
                table: "Tags",
                newName: "share");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "share",
                table: "Tags",
                newName: "Share");
        }
    }
}
