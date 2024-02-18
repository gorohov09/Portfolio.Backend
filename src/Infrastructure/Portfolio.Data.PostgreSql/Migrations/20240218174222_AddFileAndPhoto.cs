using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddFileAndPhoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_faculty_institute_institute_id",
                schema: "public",
                table: "faculty");

            migrationBuilder.CreateTable(
                name: "file",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    address = table.Column<string>(type: "text", nullable: false),
                    file_name = table.Column<string>(type: "text", nullable: false),
                    size = table.Column<long>(type: "bigint", nullable: false),
                    content_type = table.Column<string>(type: "text", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_file", x => x.id);
                },
                comment: "Файл");

            migrationBuilder.CreateTable(
                name: "photo",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    is_avatar = table.Column<bool>(type: "boolean", nullable: false, comment: "Является ли фотография аватаркой"),
                    portfolio_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор портфолио"),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор файла"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photo", x => x.id);
                    table.ForeignKey(
                        name: "fk_photo_file_file_id",
                        column: x => x.file_id,
                        principalSchema: "public",
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_photo_portfolios_portfolio_id",
                        column: x => x.portfolio_id,
                        principalSchema: "public",
                        principalTable: "portfolio",
                        principalColumn: "id");
                },
                comment: "Фотография");

            migrationBuilder.CreateIndex(
                name: "ix_photo_file_id",
                schema: "public",
                table: "photo",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_photo_portfolio_id",
                schema: "public",
                table: "photo",
                column: "portfolio_id");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_institutes_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_faculty_institutes_institute_id",
                schema: "public",
                table: "faculty");

            migrationBuilder.DropTable(
                name: "photo",
                schema: "public");

            migrationBuilder.DropTable(
                name: "file",
                schema: "public");

            migrationBuilder.AddForeignKey(
                name: "fk_faculty_institute_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id",
                principalSchema: "public",
                principalTable: "institute",
                principalColumn: "id");
        }
    }
}
