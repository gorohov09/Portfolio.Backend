using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddParticipationRelation4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "manager_user_id",
                schema: "public",
                table: "participation_activity",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_manager_user_id",
                schema: "public",
                table: "participation_activity",
                column: "manager_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_users_manager_user_id",
                schema: "public",
                table: "participation_activity",
                column: "manager_user_id",
                principalSchema: "public",
                principalTable: "user",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_users_manager_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_manager_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropColumn(
                name: "manager_user_id",
                schema: "public",
                table: "participation_activity");
        }
    }
}
