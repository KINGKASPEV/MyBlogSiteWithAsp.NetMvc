using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bloggie.Web.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class SuperAdmin1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "66b927e8-b15c-4b80-b7a5-2aab3be5dffc", "superadminkingsley@bloggie.com", "SUPERADMINKINGSLEY@BLOGGIE.COM", "SUPERADMINKINGSLEY@BLOGGIE.COM", "AQAAAAIAAYagAAAAELeWTZM/eEveUKeaR+s/blApwrzBWUA5lPvqu+NNETlTtiNSsQ75oQUJzTO20jt+Ng==", "a9308c03-d0f8-494a-85cb-37a46671c90f", "superadminkingsley@bloggie.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "d37cb911-1ee8-4b0b-b701-0af8f6e45d0e",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "1da55657-9974-46b8-bc2e-4a7284512010", "superadmin@bloggie.com", "SUPERADMIN@BLOGGIE.COM", "SUPERADMIN@BLOGGIE.COM", "AQAAAAIAAYagAAAAEH76+JdiY4MgI3KUM1Y31cb7nhLGSqmDvSeSfbJSyQdHstaaIvpux99a802MFsHJRQ==", "52d72444-1125-46b8-a82e-c6cfa20989ac", "superadmin@bloggie.com" });
        }
    }
}
