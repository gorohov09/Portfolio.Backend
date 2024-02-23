using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddParticipationRelation3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_participation_activity_document_part",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_document_files_file_id",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropPrimaryKey(
                name: "pk_participation_activity_document",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_document_file_id",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropColumn(
                name: "created_date",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropColumn(
                name: "file_id",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropColumn(
                name: "updated_date",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropColumn(
                name: "is_deleted",
                schema: "public",
                table: "file");

            migrationBuilder.AddPrimaryKey(
                name: "PK_participation_activity_document",
                schema: "public",
                table: "participation_activity_document",
                column: "id");

            migrationBuilder.CreateTable(
                name: "base_document",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, comment: "Признак удаленности"),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор файла"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_base_document", x => x.id);
                    table.ForeignKey(
                        name: "fk_base_document_file_file_id",
                        column: x => x.file_id,
                        principalSchema: "public",
                        principalTable: "file",
                        principalColumn: "id");
                },
                comment: "Базовый документ");

            migrationBuilder.CreateIndex(
                name: "ix_base_document_file_id",
                schema: "public",
                table: "base_document",
                column: "file_id");

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_base_document_participation_activity",
                schema: "public",
                table: "participation_activity",
                column: "participation_activity_document_id",
                principalSchema: "public",
                principalTable: "participation_activity_document",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_document_base_document_id",
                schema: "public",
                table: "participation_activity_document",
                column: "id",
                principalSchema: "public",
                principalTable: "base_document",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_base_document_participation_activity",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_document_base_document_id",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropTable(
                name: "base_document",
                schema: "public");

            migrationBuilder.DropPrimaryKey(
                name: "PK_participation_activity_document",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.AddColumn<DateTime>(
                name: "created_date",
                schema: "public",
                table: "participation_activity_document",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "now() at time zone 'utc'",
                comment: "Дата создания записи");

            migrationBuilder.AddColumn<Guid>(
                name: "file_id",
                schema: "public",
                table: "participation_activity_document",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                comment: "Идентификатор файла");

            migrationBuilder.AddColumn<DateTime>(
                name: "updated_date",
                schema: "public",
                table: "participation_activity_document",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                comment: "Дата изменения записи");

            migrationBuilder.AddColumn<bool>(
                name: "is_deleted",
                schema: "public",
                table: "file",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "pk_participation_activity_document",
                schema: "public",
                table: "participation_activity_document",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_document_file_id",
                schema: "public",
                table: "participation_activity_document",
                column: "file_id");

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_participation_activity_document_part",
                schema: "public",
                table: "participation_activity",
                column: "participation_activity_document_id",
                principalSchema: "public",
                principalTable: "participation_activity_document",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_document_files_file_id",
                schema: "public",
                table: "participation_activity_document",
                column: "file_id",
                principalSchema: "public",
                principalTable: "file",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
