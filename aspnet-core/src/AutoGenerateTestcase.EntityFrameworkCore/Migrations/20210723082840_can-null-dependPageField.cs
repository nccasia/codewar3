using Microsoft.EntityFrameworkCore.Migrations;

namespace AutoGenerateTestcase.Migrations
{
    public partial class cannulldependPageField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFieldConditions_PageFields_DependPageFieldId",
                table: "PageFieldConditions");

            migrationBuilder.AlterColumn<long>(
                name: "DependPageFieldId",
                table: "PageFieldConditions",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PageFieldConditions_PageFields_DependPageFieldId",
                table: "PageFieldConditions",
                column: "DependPageFieldId",
                principalTable: "PageFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageFieldConditions_PageFields_DependPageFieldId",
                table: "PageFieldConditions");

            migrationBuilder.AlterColumn<long>(
                name: "DependPageFieldId",
                table: "PageFieldConditions",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageFieldConditions_PageFields_DependPageFieldId",
                table: "PageFieldConditions",
                column: "DependPageFieldId",
                principalTable: "PageFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
