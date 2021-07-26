using Microsoft.EntityFrameworkCore.Migrations;

namespace DRIMA.Migrations
{
    public partial class Update_Request : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AbpUsers_RequestByUserId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_AbpUsers_RequestForUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestByUserId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequestForUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestByUserId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequestForUserId",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requests",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "Requests",
                type: "nvarchar(500)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reason",
                table: "Requests");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestByUserId",
                table: "Requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RequestForUserId",
                table: "Requests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestByUserId",
                table: "Requests",
                column: "RequestByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequestForUserId",
                table: "Requests",
                column: "RequestForUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AbpUsers_RequestByUserId",
                table: "Requests",
                column: "RequestByUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_AbpUsers_RequestForUserId",
                table: "Requests",
                column: "RequestForUserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
