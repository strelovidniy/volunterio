using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunterio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedContactInfoAndHelpRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ContactInfoId",
                schema: "dbo",
                table: "UserDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "CanCreateHelpRequest",
                schema: "dbo",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "CanSeeHelpRequests",
                schema: "dbo",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "ContactInfos",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    PhoneNumber = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Telegram = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Skype = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LinkedIn = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Instagram = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Other = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone('utc')"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInfos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HelpRequests",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    IssuerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Tags = table.Column<string>(type: "text", nullable: false),
                    Latitude = table.Column<double>(type: "double precision", nullable: true),
                    Longitude = table.Column<double>(type: "double precision", nullable: true),
                    ShowContactInfo = table.Column<bool>(type: "boolean", nullable: false),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone('utc')"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpRequests_Users_IssuerId",
                        column: x => x.IssuerId,
                        principalSchema: "dbo",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HelpRequestImages",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "uuid_generate_v4()"),
                    Position = table.Column<int>(type: "integer", nullable: false),
                    ImageUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ImageThumbnailUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    HelpRequestId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now() at time zone('utc')"),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HelpRequestImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HelpRequestImages_HelpRequests_HelpRequestId",
                        column: x => x.HelpRequestId,
                        principalSchema: "dbo",
                        principalTable: "HelpRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                columns: new[] { "CanCreateHelpRequest", "CanSeeHelpRequests" },
                values: new object[] { true, true });

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"),
                column: "CanCreateHelpRequest",
                value: true);

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fc1b77aa-0814-4589-80c2-a8090da02163"),
                column: "CanSeeHelpRequests",
                value: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDetails_ContactInfoId",
                schema: "dbo",
                table: "UserDetails",
                column: "ContactInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequestImages_HelpRequestId",
                schema: "dbo",
                table: "HelpRequestImages",
                column: "HelpRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_HelpRequests_IssuerId",
                schema: "dbo",
                table: "HelpRequests",
                column: "IssuerId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDetails_ContactInfos_ContactInfoId",
                schema: "dbo",
                table: "UserDetails",
                column: "ContactInfoId",
                principalSchema: "dbo",
                principalTable: "ContactInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDetails_ContactInfos_ContactInfoId",
                schema: "dbo",
                table: "UserDetails");

            migrationBuilder.DropTable(
                name: "ContactInfos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HelpRequestImages",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "HelpRequests",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_UserDetails_ContactInfoId",
                schema: "dbo",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "ContactInfoId",
                schema: "dbo",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "CanCreateHelpRequest",
                schema: "dbo",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CanSeeHelpRequests",
                schema: "dbo",
                table: "Roles");
        }
    }
}
