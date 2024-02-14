using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddCourseProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "course_project",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_in(md5(random()::text || clock_timestamp()::text)::cstring)"),
                    subject_name = table.Column<string>(type: "text", nullable: false, comment: "Наименование дисциплины"),
                    topic_name = table.Column<string>(type: "text", nullable: false, comment: "Наименование темы"),
                    semester_number = table.Column<int>(type: "integer", nullable: false, comment: "Номер семестра"),
                    score_number = table.Column<int>(type: "integer", nullable: false, comment: "Оценка"),
                    point_number = table.Column<int>(type: "integer", nullable: false, comment: "Количество баллов"),
                    сompletion_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата сдачи"),
                    portfolio_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone 'utc'", comment: "Дата создания записи"),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, comment: "Дата изменения записи")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_course_project", x => x.id);
                    table.ForeignKey(
                        name: "fk_course_project_portfolios_portfolio_id",
                        column: x => x.portfolio_id,
                        principalSchema: "public",
                        principalTable: "portfolio",
                        principalColumn: "id");
                },
                comment: "Курсовой проект");

            migrationBuilder.CreateIndex(
                name: "ix_course_project_portfolio_id",
                schema: "public",
                table: "course_project",
                column: "portfolio_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "course_project",
                schema: "public");
        }
    }
}
