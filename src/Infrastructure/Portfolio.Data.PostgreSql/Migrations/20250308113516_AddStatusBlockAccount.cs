using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddStatusBlockAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_blocked",
                schema: "public",
                table: "user",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                comment: "Статус аккаунта");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_blocked",
                schema: "public",
                table: "user");
        }
    }
}
