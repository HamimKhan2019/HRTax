using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    HouseRentAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MedicalAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConveyanceAllowance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PfContributionPercent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "BasicSalary", "ConveyanceAllowance", "Department", "Designation", "HouseRentAllowance", "MedicalAllowance", "Name", "PfContributionPercent" },
                values: new object[,]
                {
                    { 1, 50000m, 3000m, "IT", "Software Engineer", 25000m, 5000m, "Abir Hossain", 10m },
                    { 2, 80000m, 4000m, "IT", "Senior Engineer", 40000m, 6000m, "Nusrat Jahan", 10m },
                    { 3, 70000m, 3500m, "HR", "HR Manager", 35000m, 5000m, "Karim Ahmed", 10m },
                    { 4, 45000m, 2500m, "Finance", "Accountant", 22000m, 4000m, "Fatima Akter", 8m },
                    { 5, 120000m, 5000m, "IT", "Project Manager", 60000m, 8000m, "Tanvir Rahman", 10m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
