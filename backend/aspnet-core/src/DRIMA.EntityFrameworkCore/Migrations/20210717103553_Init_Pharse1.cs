using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DRIMA.Migrations
{
    public partial class Init_Pharse1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    WarrantyDate = table.Column<DateTime>(nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    BorrowedDate = table.Column<DateTime>(nullable: true),
                    BorrowedUserId = table.Column<long>(nullable: true),
                    QrCode = table.Column<string>(nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_AbpUsers_BorrowedUserId",
                        column: x => x.BorrowedUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Base64 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DeviceId = table.Column<Guid>(nullable: true),
                    Type = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceImages_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    RequestByUserId = table.Column<long>(nullable: true),
                    RequestForUserId = table.Column<long>(nullable: true),
                    Status = table.Column<byte>(type: "tinyint", nullable: false),
                    Type = table.Column<byte>(type: "tinyint", nullable: false),
                    DeviceId = table.Column<Guid>(nullable: true),
                    Title = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Priority = table.Column<byte>(type: "tinyint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_AbpUsers_RequestByUserId",
                        column: x => x.RequestByUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_AbpUsers_RequestForUserId",
                        column: x => x.RequestForUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceImages_DeviceId",
                table: "DeviceImages",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_BorrowedUserId",
                table: "Devices",
                column: "BorrowedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DeviceId",
                table: "Requests",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestByUserId",
                table: "Requests",
                column: "RequestByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestForUserId",
                table: "Requests",
                column: "RequestForUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceImages");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
