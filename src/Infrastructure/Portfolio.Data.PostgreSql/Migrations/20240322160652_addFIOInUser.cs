using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class addFIOInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "first_name",
                schema: "public",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Имя");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                schema: "public",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "",
                comment: "Фамилия");

            migrationBuilder.AddColumn<string>(
                name: "surname",
                schema: "public",
                table: "user",
                type: "text",
                nullable: true,
                comment: "Отчество");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "first_name",
                schema: "public",
                table: "user");

            migrationBuilder.DropColumn(
                name: "last_name",
                schema: "public",
                table: "user");

            migrationBuilder.DropColumn(
                name: "surname",
                schema: "public",
                table: "user");
        }
    }
}
