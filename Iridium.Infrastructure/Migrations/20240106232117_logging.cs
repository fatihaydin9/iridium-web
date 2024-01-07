using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Iridium.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class logging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LogLevel = table.Column<short>(type: "smallint", nullable: false),
                    LogType = table.Column<short>(type: "smallint", nullable: false),
                    ServiceType = table.Column<short>(type: "smallint", nullable: false),
                    Key = table.Column<long>(type: "bigint", nullable: true),
                    KeyName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    InComing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OutGoing = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIp = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ServerName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ResponseStart = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponseEnd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceType = table.Column<byte>(type: "tinyint", nullable: true),
                    GuidId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<long>(type: "bigint", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Log");
        }
    }
}
