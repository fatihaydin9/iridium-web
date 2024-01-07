using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iridium.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class extensive : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "Password",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "UserKey",
                table: "Password",
                newName: "Salt");

            migrationBuilder.AddColumn<long>(
                name: "ParentRoleId",
                table: "Role",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Password",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentRoleId",
                table: "Role");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Password",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Salt",
                table: "Password",
                newName: "UserKey");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Password",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Category",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
