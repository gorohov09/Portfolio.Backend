using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class addBucketToFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "bucket",
                schema: "public",
                table: "file",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bucket",
                schema: "public",
                table: "file");
        }
    }
}
