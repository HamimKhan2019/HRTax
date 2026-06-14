using Microsoft.EntityFrameworkCore;
using EmployeeService.Domain;

namespace EmployeeService.Infrastructure;

public class EmployeeDbContext : DbContext
{
    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options) { }
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "Abir Hossain", Designation = "Software Engineer", Department = "IT", BasicSalary = 50000, HouseRentAllowance = 25000, MedicalAllowance = 5000, ConveyanceAllowance = 3000, PfContributionPercent = 10 },
            new Employee { Id = 2, Name = "Nusrat Jahan", Designation = "Senior Engineer", Department = "IT", BasicSalary = 80000, HouseRentAllowance = 40000, MedicalAllowance = 6000, ConveyanceAllowance = 4000, PfContributionPercent = 10 },
            new Employee { Id = 3, Name = "Karim Ahmed", Designation = "HR Manager", Department = "HR", BasicSalary = 70000, HouseRentAllowance = 35000, MedicalAllowance = 5000, ConveyanceAllowance = 3500, PfContributionPercent = 10 },
            new Employee { Id = 4, Name = "Fatima Akter", Designation = "Accountant", Department = "Finance", BasicSalary = 45000, HouseRentAllowance = 22000, MedicalAllowance = 4000, ConveyanceAllowance = 2500, PfContributionPercent = 8 },
            new Employee { Id = 5, Name = "Tanvir Rahman", Designation = "Project Manager", Department = "IT", BasicSalary = 120000, HouseRentAllowance = 60000, MedicalAllowance = 8000, ConveyanceAllowance = 5000, PfContributionPercent = 10 }
        );
    }
}