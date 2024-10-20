using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    /// <inheritdoc />
    public partial class DocumentTableColumsUpdatedForSaveDocumentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "documentExtension",
                table: "documentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "documentSize",
                table: "documentModels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "uploadedBy",
                table: "documentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "uploaded_DateTime",
                table: "documentModels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "documentExtension",
                table: "documentModels");

            migrationBuilder.DropColumn(
                name: "documentSize",
                table: "documentModels");

            migrationBuilder.DropColumn(
                name: "uploadedBy",
                table: "documentModels");

            migrationBuilder.DropColumn(
                name: "uploaded_DateTime",
                table: "documentModels");
        }
    }
}
