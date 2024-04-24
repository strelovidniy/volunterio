using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Volunterio.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Roles",
                columns: new[] { "Id", "CanCreateRoles", "CanDeleteRoles", "CanDeleteUsers", "CanEditRoles", "CanEditUsers", "CanInviteUsers", "CanMaintainSystem", "CanRestoreUsers", "CanSeeAllRoles", "CanSeeAllUsers", "CanSeeRoles", "CanSeeUsers", "CreatedAt", "DeletedAt", "IsHidden", "Name", "Type", "UpdatedAt" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, true, true, true, true, true, true, true, true, true, true, true, new DateTime(2022, 11, 11, 1, 6, 0, 0, DateTimeKind.Utc), null, true, "Admin", "Admin", null });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Roles",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "IsHidden", "Name", "Type", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"), new DateTime(2022, 11, 11, 1, 6, 0, 0, DateTimeKind.Utc), null, true, "User", "User", null },
                    { new Guid("fc1b77aa-0814-4589-80c2-a8090da02163"), new DateTime(2022, 11, 11, 1, 6, 0, 0, DateTimeKind.Utc), null, true, "Helper", "Helper", null }
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAt", "RegistrationToken", "RoleId", "Status", "UpdatedAt", "VerificationCode" },
                values: new object[,]
                {
                    { new Guid("00a68a21-7b01-2211-abbc-0022480a1c03"), null, "roma.dan2001@gmail.com", "Roman", "Danylevych", "2576c639ea2309626fee6232e624ba921afada44537b9fa6592f03d5a1da7dd375fbd17b2af56655323327e8fd75a46d4932d54c4df61595844bc95fd5979c56", null, null, null, new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"), "Active", null, null },
                    { new Guid("6ebd3c1c-4a07-4c53-8a71-493386f28261"), null, "nazariy.chetvertukha@gmail.com", "Nazariy", "Chetvertukha", "2576c639ea2309626fee6232e624ba921afada44537b9fa6592f03d5a1da7dd375fbd17b2af56655323327e8fd75a46d4932d54c4df61595844bc95fd5979c56", null, null, null, new Guid("fc1b77aa-0814-4589-80c2-a8090da02163"), "Active", null, null },
                    { new Guid("bafee45c-1874-4c40-b781-5221133b4c30"), null, "annstepaniuk12@gmail.com", "Ann", "Stepaniuk", "2576c639ea2309626fee6232e624ba921afada44537b9fa6592f03d5a1da7dd375fbd17b2af56655323327e8fd75a46d4932d54c4df61595844bc95fd5979c56", null, null, null, new Guid("fc1b77aa-0814-4589-80c2-a8090da02163"), "Active", null, null },
                    { new Guid("e823f508-ce4e-498f-a401-dbf40c892dbb"), null, "roman.sadokha01@gmail.com", "Roman", "Sadokha", "2576c639ea2309626fee6232e624ba921afada44537b9fa6592f03d5a1da7dd375fbd17b2af56655323327e8fd75a46d4932d54c4df61595844bc95fd5979c56", null, null, null, new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"), "Active", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00a68a21-7b01-2211-abbc-0022480a1c03"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6ebd3c1c-4a07-4c53-8a71-493386f28261"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("bafee45c-1874-4c40-b781-5221133b4c30"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e823f508-ce4e-498f-a401-dbf40c892dbb"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"));

            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("fc1b77aa-0814-4589-80c2-a8090da02163"));
        }
    }
}
