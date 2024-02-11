using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddPortfolio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "institute",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    full_name = table.Column<string>(type: "text", nullable: false, comment: "Полное имя"),
                    short_name = table.Column<string>(type: "text", nullable: false, comment: "Сокращенное имя"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institute", x => x.id);
                },
                comment: "Институт");

            migrationBuilder.CreateTable(
                name: "role",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    name = table.Column<string>(type: "text", nullable: false, comment: "Наименование"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role", x => x.id);
                },
                comment: "Роль");

            migrationBuilder.CreateTable(
                name: "faculty",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    full_name = table.Column<string>(type: "text", nullable: false, comment: "Полное имя"),
                    short_name = table.Column<string>(type: "text", nullable: false, comment: "Сокращенное имя"),
                    institute_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор института"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_faculty", x => x.id);
                    table.ForeignKey(
                        name: "fk_faculty_institute_institute_id",
                        column: x => x.institute_id,
                        principalSchema: "public",
                        principalTable: "institute",
                        principalColumn: "id");
                },
                comment: "Кафедра");

            migrationBuilder.CreateTable(
                name: "role_privilege",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли"),
                    privilege = table.Column<int>(type: "integer", nullable: false, comment: "Право доступа"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_privilege", x => x.id);
                    table.ForeignKey(
                        name: "fk_role_privilege_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "id");
                },
                comment: "Право доступа для роли");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    login = table.Column<string>(type: "text", nullable: false, comment: "Логин"),
                    password_hash = table.Column<string>(type: "text", nullable: false, comment: "Хеш пароля"),
                    email = table.Column<string>(type: "text", nullable: false, comment: "Электронная почта"),
                    phone = table.Column<string>(type: "text", nullable: true, comment: "Телефон"),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор роли"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_role_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "role",
                        principalColumn: "id");
                },
                comment: "Пользователь");

            migrationBuilder.CreateTable(
                name: "portfolio",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    last_name = table.Column<string>(type: "text", nullable: false, comment: "Фамилия"),
                    first_name = table.Column<string>(type: "text", nullable: false, comment: "Имя"),
                    surname = table.Column<string>(type: "text", nullable: true, comment: "Отчество"),
                    birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата рождения"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Идентификатор пользователя"),
                    education_level = table.Column<int>(type: "integer", nullable: true, comment: "Уровень образования"),
                    group_number = table.Column<string>(type: "text", nullable: true, comment: "Номер группы"),
                    speciality_name = table.Column<string>(type: "text", nullable: true, comment: "Название"),
                    speciality_number = table.Column<string>(type: "text", nullable: true, comment: "Номер"),
                    faculty_id = table.Column<Guid>(type: "uuid", nullable: true, comment: "Идентификатор кафедры"),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_portfolio", x => x.id);
                    table.ForeignKey(
                        name: "fk_portfolio_faculty_faculty_id",
                        column: x => x.faculty_id,
                        principalSchema: "public",
                        principalTable: "faculty",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_portfolio_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "user",
                        principalColumn: "id");
                },
                comment: "Портфолио");

            migrationBuilder.CreateIndex(
                name: "ix_faculty_institute_id",
                schema: "public",
                table: "faculty",
                column: "institute_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_faculty_id",
                schema: "public",
                table: "portfolio",
                column: "faculty_id");

            migrationBuilder.CreateIndex(
                name: "ix_portfolio_user_id",
                schema: "public",
                table: "portfolio",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_role_privilege_role_id",
                schema: "public",
                table: "role_privilege",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_role_id",
                schema: "public",
                table: "user",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolio",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role_privilege",
                schema: "public");

            migrationBuilder.DropTable(
                name: "faculty",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user",
                schema: "public");

            migrationBuilder.DropTable(
                name: "institute",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role",
                schema: "public");
        }
    }
}
