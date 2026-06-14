using MediatR;
using TaxService.Application.Dtos;
using TaxService.Application.Interfaces;
using TaxService.Domain;

namespace TaxService.Application.Queries;

public class GetEmployeeTaxQueryHandler : IRequestHandler<GetEmployeeTaxQuery, TaxResultDto?>
{
    private readonly IEmployeeGrpcClient _employeeClient;
    private readonly ITaxEventPublisher _eventPublisher;
    private readonly IAuditLogger _auditLogger;

    public GetEmployeeTaxQueryHandler(IEmployeeGrpcClient employeeClient, ITaxEventPublisher eventPublisher, IAuditLogger auditLogger)
    {
        _employeeClient = employeeClient;
        _eventPublisher = eventPublisher;
        _auditLogger = auditLogger;
    }

    public async Task<TaxResultDto?> Handle(GetEmployeeTaxQuery request, CancellationToken cancellationToken)
    {
        var emp = await _employeeClient.GetEmployeeAsync(request.EmployeeId);
        if (!emp.Found) return null;

        decimal monthlyGross = emp.BasicSalary + emp.HouseRent + emp.Medical + emp.Conveyance;
        decimal annualGross = monthlyGross * 12;
        decimal monthlyPf = emp.BasicSalary * (emp.PfPercent / 100m);
        decimal annualPf = monthlyPf * 12;
        decimal annualTaxableIncome = annualGross - annualPf;
        if (annualTaxableIncome < 0) annualTaxableIncome = 0;

        var (breakdown, totalTax) = TaxSlabCalculator.Calculate(annualTaxableIncome);
        decimal monthlyTax = totalTax / 12;
        decimal monthlyNet = monthlyGross - monthlyPf - monthlyTax;

        var result = new TaxResultDto
        {
            EmployeeId = emp.Id,
            Name = emp.Name,
            Designation = emp.Designation,
            Department = emp.Department,
            MonthlyGrossSalary = monthlyGross,
            AnnualGrossSalary = annualGross,
            MonthlyPfDeduction = monthlyPf,
            AnnualPfDeduction = annualPf,
            AnnualTaxableIncome = annualTaxableIncome,
            SlabBreakdown = breakdown,
            TotalAnnualTax = totalTax,
            MonthlyTax = monthlyTax,
            MonthlyNetSalary = monthlyNet
        };

        await _auditLogger.LogAsync("CalculateTax", emp.Id, $"AnnualTax={totalTax}");
        await _eventPublisher.PublishTaxCalculatedAsync(emp.Id, totalTax);

        return result;
    }
}