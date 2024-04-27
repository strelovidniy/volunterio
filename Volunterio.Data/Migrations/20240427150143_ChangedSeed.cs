using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volunterio.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00a68a21-7b01-2211-abbc-0022480a1c03"),
                column: "RoleId",
                value: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "Users",
                columns: new[] { "Id", "DeletedAt", "Email", "FirstName", "LastName", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAt", "RegistrationToken", "RoleId", "Status", "UpdatedAt", "VerificationCode" },
                values: new object[] { new Guid("07afd050-0126-4610-8dae-854efa9fcfde"), null, "boredarthur@gmail.com", "Arthur", "Zavolovych", "2576c639ea2309626fee6232e624ba921afada44537b9fa6592f03d5a1da7dd375fbd17b2af56655323327e8fd75a46d4932d54c4df61595844bc95fd5979c56", null, null, null, new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"), "Active", null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("07afd050-0126-4610-8dae-854efa9fcfde"));

            migrationBuilder.UpdateData(
                schema: "dbo",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00a68a21-7b01-2211-abbc-0022480a1c03"),
                column: "RoleId",
                value: new Guid("a0a80c03-abbc-eb11-cabb-0022480a1c0a"));
        }
    }
}
