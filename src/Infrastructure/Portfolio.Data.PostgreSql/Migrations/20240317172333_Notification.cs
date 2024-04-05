using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class Notification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    type = table.Column<int>(type: "integer", nullable: false, comment: "Тип уведомления"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя-получателя"),
                    is_read = table.Column<bool>(type: "boolean", nullable: false, comment: "Является ли уведомление прочитанным"),
                    title = table.Column<string>(type: "text", nullable: false, comment: "Заголовок"),
                    description = table.Column<string>(type: "text", nullable: true, comment: "Описание"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_notification", x => x.id);
                    table.ForeignKey(
                        name: "fk_notification_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                },
                comment: "Уведомление");

            migrationBuilder.CreateIndex(
                name: "ix_notification_user_id",
                schema: "public",
                table: "notification",
                column: "user_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification",
                schema: "public");
        }
    }
}
