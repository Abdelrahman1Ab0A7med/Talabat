using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_deleiveryMethods_DeleiveryMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_deleiveryMethods",
                table: "deleiveryMethods");

            migrationBuilder.RenameTable(
                name: "deleiveryMethods",
                newName: "DeleiveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DeleiveryMethods",
                table: "DeleiveryMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeleiveryMethods_DeleiveryMethodId",
                table: "Orders",
                column: "DeleiveryMethodId",
                principalTable: "DeleiveryMethods",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeleiveryMethods_DeleiveryMethodId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DeleiveryMethods",
                table: "DeleiveryMethods");

            migrationBuilder.RenameTable(
                name: "DeleiveryMethods",
                newName: "deleiveryMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_deleiveryMethods",
                table: "deleiveryMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_deleiveryMethods_DeleiveryMethodId",
                table: "Orders",
                column: "DeleiveryMethodId",
                principalTable: "deleiveryMethods",
                principalColumn: "Id");
        }
    }
}
