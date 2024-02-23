using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddParticipation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "activity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Название"),
                    section = table.Column<int>(type: "integer", nullable: false, comment: "Вид"),
                    type = table.Column<int>(type: "integer", nullable: false, comment: "Тип"),
                    level = table.Column<int>(type: "integer", nullable: false, comment: "Уровень"),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата начала"),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата окончания"),
                    location = table.Column<string>(type: "text", nullable: true, comment: "Место"),
                    link = table.Column<string>(type: "text", nullable: true, comment: "Ссылка на официальную информацию"),
                    description = table.Column<string>(type: "text", nullable: true, comment: "Описание"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_activity", x => x.id);
                },
                comment: "Мероприятие");

            migrationBuilder.CreateTable(
                name: "participation_activity",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    status = table.Column<int>(type: "integer", nullable: false, comment: "Статус"),
                    result = table.Column<int>(type: "integer", nullable: true, comment: "Результат"),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, comment: "Дата участия"),
                    description = table.Column<string>(type: "text", nullable: true, comment: "Описание участия"),
                    comment = table.Column<string>(type: "text", nullable: true, comment: "Комментарий от администратора"),
                    activity_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор мероприятия"),
                    portfolio_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор портфолио"),
                    participation_activity_document_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор подтверждающего документа участия в мероприятии"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participation_activity", x => x.id);
                    table.ForeignKey(
                        name: "fk_participation_activity_activity_activity_id",
                        column: x => x.activity_id,
                        principalSchema: "public",
                        principalTable: "activity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_participation_activity_portfolios_portfolio_id",
                        column: x => x.portfolio_id,
                        principalSchema: "public",
                        principalTable: "portfolio",
                        principalColumn: "id");
                },
                comment: "Участие в мероприятии");

            migrationBuilder.CreateTable(
                name: "participation_activity_document",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    type = table.Column<int>(type: "integer", nullable: false, comment: "Тип подтверждающего документа участия"),
                    participation_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор участия в мероприятии"),
                    file_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор файла"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_participation_activity_document", x => x.id);
                    table.ForeignKey(
                        name: "fk_participation_activity_document_files_file_id",
                        column: x => x.file_id,
                        principalSchema: "public",
                        principalTable: "file",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_participation_activity_document_participation_activity_part",
                        column: x => x.participation_id,
                        principalSchema: "public",
                        principalTable: "participation_activity",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "Подтверждающий документ участия в мероприятии");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_activity_id",
                schema: "public",
                table: "participation_activity",
                column: "activity_id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_portfolio_id",
                schema: "public",
                table: "participation_activity",
                column: "portfolio_id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_document_file_id",
                schema: "public",
                table: "participation_activity_document",
                column: "file_id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_document_participation_id",
                schema: "public",
                table: "participation_activity_document",
                column: "participation_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "participation_activity_document",
                schema: "public");

            migrationBuilder.DropTable(
                name: "participation_activity",
                schema: "public");

            migrationBuilder.DropTable(
                name: "activity",
                schema: "public");
        }
    }
}
