using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    /// <inheritdoc />
    public partial class errorTableAddedToContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "errorLogDetails",
                columns: table => new
                {
                    logID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    errorMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    errorLogOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_errorLogDetails", x => x.logID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "errorLogDetails");
        }
    }
}
