using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "documentModels",
                columns: table => new
                {
                    documentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    documentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    documentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    uploadedFile = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_documentModels", x => x.documentID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "documentModels");
        }
    }
}
