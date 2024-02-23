﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Data.PostgreSql.Migrations
{
    public partial class AddParticipationRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_document_participation_activity_part",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_document_participation_id",
                schema: "public",
                table: "participation_activity_document");

            migrationBuilder.AlterColumn<Guid>(
                name: "participation_id",
                schema: "public",
                table: "participation_activity_document",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldComment: "Идентификатор участия в мероприятии");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_participation_activity_document_id",
                schema: "public",
                table: "participation_activity",
                column: "participation_activity_document_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_participation_activity_document_part",
                schema: "public",
                table: "participation_activity",
                column: "participation_activity_document_id",
                principalSchema: "public",
                principalTable: "participation_activity_document",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_participation_activity_participation_activity_document_part",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.DropIndex(
                name: "ix_participation_activity_participation_activity_document_id",
                schema: "public",
                table: "participation_activity");

            migrationBuilder.AlterColumn<Guid>(
                name: "participation_id",
                schema: "public",
                table: "participation_activity_document",
                type: "uuid",
                nullable: false,
                comment: "Идентификатор участия в мероприятии",
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.CreateIndex(
                name: "ix_participation_activity_document_participation_id",
                schema: "public",
                table: "participation_activity_document",
                column: "participation_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_participation_activity_document_participation_activity_part",
                schema: "public",
                table: "participation_activity_document",
                column: "participation_id",
                principalSchema: "public",
                principalTable: "participation_activity",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
