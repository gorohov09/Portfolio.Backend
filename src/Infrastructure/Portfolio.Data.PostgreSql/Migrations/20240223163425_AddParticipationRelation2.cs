using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddParticipationRelation2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "created_by_user_id",
                schema: "public",
                table: "participation_activity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "modified_by_user_id",
                schema: "public",
                table: "participation_activity",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_created_by_user_id",
                schema: "public",
                table: "participation_activity",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_modified_by_user_id",
                schema: "public",
                table: "participation_activity",
                column: "modified_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_users_created_by_user_id",
                schema: "public",
                table: "participation_activity",
                column: "created_by_user_id",
                principalSchema: "public",
                principalTable: "user",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_users_modified_by_user_id",
                schema: "public",
                table: "participation_activity",
                column: "modified_by_user_id",
                principalSchema: "public",
                principalTable: "user",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_users_created_by_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_users_modified_by_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_created_by_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_modified_by_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropColumn(
                name: "modified_by_user_id",
                schema: "public",
                table: "participation_activity");
        }
    }
}
