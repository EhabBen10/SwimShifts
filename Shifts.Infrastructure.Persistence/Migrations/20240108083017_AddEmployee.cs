using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shifts.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "WaterSamples",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imgUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WaterSamples_EmployeeId",
                table: "WaterSamples",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WaterSamples_Employees_EmployeeId",
                table: "WaterSamples",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WaterSamples_Employees_EmployeeId",
                table: "WaterSamples");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_WaterSamples_EmployeeId",
                table: "WaterSamples");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "WaterSamples");
        }
    }
}
