using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using EmployeeService.Api.Protos;
using EmployeeService.Infrastructure;

namespace EmployeeService.Api.Services;

public class EmployeeGrpcService : EmployeeGrpc.EmployeeGrpcBase
{
    private readonly EmployeeDbContext _db;
    public EmployeeGrpcService(EmployeeDbContext db) => _db = db;

    public override async Task<EmployeeReply> GetEmployee(EmployeeRequest request, ServerCallContext context)
    {
        var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == request.Id);
        if (emp == null) return new EmployeeReply { Found = false };

        return new EmployeeReply
        {
            Found = true,
            Id = emp.Id,
            Name = emp.Name,
            Designation = emp.Designation,
            Department = emp.Department,
            BasicSalary = (double)emp.BasicSalary,
            HouseRent = (double)emp.HouseRentAllowance,
            Medical = (double)emp.MedicalAllowance,
            Conveyance = (double)emp.ConveyanceAllowance,
            PfPercent = (double)emp.PfContributionPercent
        };
    }
}