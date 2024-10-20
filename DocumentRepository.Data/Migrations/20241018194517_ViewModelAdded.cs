using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentRepository.Data.Migrations
{
    /// <inheritdoc />
    public partial class ViewModelAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploadedFile",
                table: "documentModels",
                newName: "uploadedFileDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "uploadedFileDetails",
                table: "documentModels",
                newName: "uploadedFile");
        }
    }
}
