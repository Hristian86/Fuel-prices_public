using Microsoft.EntityFrameworkCore.Migrations;

namespace AkciqApp.Data.Migrations
{
    public partial class Seeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IpAddress_AspNetUsers_UserId",
                table: "IpAddress");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress");

            migrationBuilder.RenameTable(
                name: "IpAddress",
                newName: "ipAddresses");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddress_UserId",
                table: "ipAddresses",
                newName: "IX_ipAddresses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_IpAddress_IsDeleted",
                table: "ipAddresses",
                newName: "IX_ipAddresses_IsDeleted");

            migrationBuilder.AddColumn<string>(
                name: "Info",
                table: "ipAddresses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Visits",
                table: "ipAddresses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ipAddresses",
                table: "ipAddresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ipAddresses_AspNetUsers_UserId",
                table: "ipAddresses",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ipAddresses_AspNetUsers_UserId",
                table: "ipAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ipAddresses",
                table: "ipAddresses");

            migrationBuilder.DropColumn(
                name: "Info",
                table: "ipAddresses");

            migrationBuilder.DropColumn(
                name: "Visits",
                table: "ipAddresses");

            migrationBuilder.RenameTable(
                name: "ipAddresses",
                newName: "IpAddress");

            migrationBuilder.RenameIndex(
                name: "IX_ipAddresses_UserId",
                table: "IpAddress",
                newName: "IX_IpAddress_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ipAddresses_IsDeleted",
                table: "IpAddress",
                newName: "IX_IpAddress_IsDeleted");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IpAddress",
                table: "IpAddress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IpAddress_AspNetUsers_UserId",
                table: "IpAddress",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
