using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DRIMA.Migrations
{
    public partial class update_name_guarantee_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WarrantyDate",
                table: "Devices");

            migrationBuilder.AddColumn<DateTime>(
                name: "GuaranteeDate",
                table: "Devices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GuaranteeDate",
                table: "Devices");

            migrationBuilder.AddColumn<DateTime>(
                name: "WarrantyDate",
                table: "Devices",
                type: "datetime2",
                nullable: true);
        }
    }
}
