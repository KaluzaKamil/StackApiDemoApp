using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StackApiDemo.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TagsImports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    has_more = table.Column<bool>(type: "bit", nullable: false),
                    quota_max = table.Column<int>(type: "int", nullable: false),
                    quota_remaining = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsImports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    has_synonyms = table.Column<bool>(type: "bit", nullable: false),
                    is_moderator_only = table.Column<bool>(type: "bit", nullable: false),
                    is_required = table.Column<bool>(type: "bit", nullable: false),
                    count = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagsImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_TagsImports_TagsImportId",
                        column: x => x.TagsImportId,
                        principalTable: "TagsImports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Collectives",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    slug = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Collectives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Collectives_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ExternalLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    link = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CollectiveId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalLinks_Collectives_CollectiveId",
                        column: x => x.CollectiveId,
                        principalTable: "Collectives",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Collectives_TagId",
                table: "Collectives",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalLinks_CollectiveId",
                table: "ExternalLinks",
                column: "CollectiveId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_TagsImportId",
                table: "Tags",
                column: "TagsImportId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalLinks");

            migrationBuilder.DropTable(
                name: "Collectives");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "TagsImports");
        }
    }
}
