using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class Add_EmailMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "email_message",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    address_to = table.Column<string>(type: "text", nullable: false, comment: "Адрес получателя"),
                    subject = table.Column<string>(type: "text", nullable: false, comment: "Заголовок сообщения"),
                    body = table.Column<string>(type: "text", nullable: false, comment: "Тело сообщения"),
                    to_user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Id пользователя-получателя"),
                    is_sent = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_email_message", x => x.id);
                },
                comment: "Электронно-почтовое сообщение");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "email_message",
                schema: "public");
        }
    }
}
