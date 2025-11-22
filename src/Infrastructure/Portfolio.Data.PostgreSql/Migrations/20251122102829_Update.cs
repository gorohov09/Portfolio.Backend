using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_base_document_file_file_id",
                schema: "public",
                table: "base_document");

            migrationBuilder.AddForeignKey(
                name: "fk_base_document_files_file_id",
                schema: "public",
                table: "base_document",
                column: "file_id",
                principalSchema: "public",
                principalTable: "file",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_base_document_files_file_id",
                schema: "public",
                table: "base_document");

            migrationBuilder.AddForeignKey(
                name: "fk_base_document_file_file_id",
                schema: "public",
                table: "base_document",
                column: "file_id",
                principalSchema: "public",
                principalTable: "file",
                principalColumn: "id");
        }
    }
}
