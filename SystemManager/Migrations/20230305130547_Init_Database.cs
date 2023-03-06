using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SystemManager.Migrations
{
    /// <inheritdoc />
    public partial class Init_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cases_Employees_EmployeeId",
                table: "Cases");

            migrationBuilder.DropIndex(
                name: "IX_Cases_EmployeeId",
                table: "Cases");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Cases");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_EmployeeId",
                table: "Comments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Employees_EmployeeId",
                table: "Comments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Employees_EmployeeId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_EmployeeId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Comments");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Cases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_EmployeeId",
                table: "Cases",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cases_Employees_EmployeeId",
                table: "Cases",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
