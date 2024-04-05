using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddPeriod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start_date",
                schema: "public",
                table: "activity",
                newName: "period_start_date");

            migrationBuilder.RenameColumn(
                name: "end_date",
                schema: "public",
                table: "activity",
                newName: "period_end_date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "period_start_date",
                schema: "public",
                table: "activity",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "period_end_date",
                schema: "public",
                table: "activity",
                newName: "end_date");
        }
    }
}
