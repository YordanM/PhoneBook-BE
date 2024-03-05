using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhoneBook.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedContacts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b2f770dd-aad6-4b3b-9ff1-1e5668ee528c");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1fe828fc-4cd9-4ff1-a05b-82f058150e8b", null, "User", "USER" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1fe828fc-4cd9-4ff1-a05b-82f058150e8b");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Contacts");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b2f770dd-aad6-4b3b-9ff1-1e5668ee528c", null, "User", "USER" });
        }
    }
}
