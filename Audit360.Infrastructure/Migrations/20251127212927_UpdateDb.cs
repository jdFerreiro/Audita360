using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Audit360.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_AuditStatuses_StatusId",
                table: "Audits");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_FollowUpStatuses_StatusId",
                table: "FollowUps");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "FollowUps",
                newName: "FollowUpStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUps_StatusId",
                table: "FollowUps",
                newName: "IX_FollowUps_FollowUpStatusId");

            migrationBuilder.RenameColumn(
                name: "StatusId",
                table: "Audits",
                newName: "AuditStatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Audits_StatusId",
                table: "Audits",
                newName: "IX_Audits_AuditStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_AuditStatuses_AuditStatusId",
                table: "Audits",
                column: "AuditStatusId",
                principalTable: "AuditStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_FollowUpStatuses_FollowUpStatusId",
                table: "FollowUps",
                column: "FollowUpStatusId",
                principalTable: "FollowUpStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audits_AuditStatuses_AuditStatusId",
                table: "Audits");

            migrationBuilder.DropForeignKey(
                name: "FK_FollowUps_FollowUpStatuses_FollowUpStatusId",
                table: "FollowUps");

            migrationBuilder.RenameColumn(
                name: "FollowUpStatusId",
                table: "FollowUps",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_FollowUps_FollowUpStatusId",
                table: "FollowUps",
                newName: "IX_FollowUps_StatusId");

            migrationBuilder.RenameColumn(
                name: "AuditStatusId",
                table: "Audits",
                newName: "StatusId");

            migrationBuilder.RenameIndex(
                name: "IX_Audits_AuditStatusId",
                table: "Audits",
                newName: "IX_Audits_StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Audits_AuditStatuses_StatusId",
                table: "Audits",
                column: "StatusId",
                principalTable: "AuditStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FollowUps_FollowUpStatuses_StatusId",
                table: "FollowUps",
                column: "StatusId",
                principalTable: "FollowUpStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
