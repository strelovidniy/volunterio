using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunterio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationSettingsRelationConfig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_NotificationSettings_UserId",
                schema: "dbo",
                table: "NotificationSettings",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationSettings_Users_UserId",
                schema: "dbo",
                table: "NotificationSettings",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationSettings_Users_UserId",
                schema: "dbo",
                table: "NotificationSettings");

            migrationBuilder.DropIndex(
                name: "IX_NotificationSettings_UserId",
                schema: "dbo",
                table: "NotificationSettings");
        }
    }
}
