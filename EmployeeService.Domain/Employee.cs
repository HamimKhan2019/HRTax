namespace EmployeeService.Domain;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Designation { get; set; } = "";
    public string Department { get; set; } = "";
    public decimal BasicSalary { get; set; }
    public decimal HouseRentAllowance { get; set; }
    public decimal MedicalAllowance { get; set; }
    public decimal ConveyanceAllowance { get; set; }
    public decimal PfContributionPercent { get; set; }
}