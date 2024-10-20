using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateByColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "updatedBy",
                table: "documentModels",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedOn",
                table: "documentModels",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "updatedBy",
                table: "documentModels");

            migrationBuilder.DropColumn(
                name: "updatedOn",
                table: "documentModels");
        }
    }
}
