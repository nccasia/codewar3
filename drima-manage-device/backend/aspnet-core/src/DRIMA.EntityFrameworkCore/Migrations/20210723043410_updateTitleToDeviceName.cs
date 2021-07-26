using Microsoft.EntityFrameworkCore.Migrations;

namespace DRIMA.Migrations
{
    public partial class updateTitleToDeviceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Requests",
                newName: "DeviceName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeviceName",
                table: "Requests",
                newName: "Title");
        }
    }
}
