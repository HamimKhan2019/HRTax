namespace TaxService.Application.Interfaces;

public record EmployeeInfo(bool Found, int Id, string Name, string Designation, string Department,
    decimal BasicSalary, decimal HouseRent, decimal Medical, decimal Conveyance, decimal PfPercent);

public interface IEmployeeGrpcClient
{
    Task<EmployeeInfo> GetEmployeeAsync(int id);
}