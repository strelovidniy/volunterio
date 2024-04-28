using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunterio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedNotificationSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationSettings",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnableNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    EnableTagFiltration = table.Column<bool>(type: "boolean", nullable: false),
                    FilterTags = table.Column<string>(type: "text", nullable: true),
                    EnableTitleFiltration = table.Column<bool>(type: "boolean", nullable: false),
                    FilterTitles = table.Column<string>(type: "text", nullable: true),
                    EnableUpdateNotifications = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone('utc')"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSettings", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSettings",
                schema: "dbo");
        }
    }
}
