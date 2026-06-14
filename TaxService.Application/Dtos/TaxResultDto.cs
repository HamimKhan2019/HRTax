using TaxService.Domain;

namespace TaxService.Application.Dtos;

public class TaxResultDto
{
    public int EmployeeId { get; set; }
    public string Name { get; set; } = "";
    public string Designation { get; set; } = "";
    public string Department { get; set; } = "";
    public decimal MonthlyGrossSalary { get; set; }
    public decimal AnnualGrossSalary { get; set; }
    public decimal MonthlyPfDeduction { get; set; }
    public decimal AnnualPfDeduction { get; set; }
    public decimal AnnualTaxableIncome { get; set; }
    public List<SlabBreakdown> SlabBreakdown { get; set; } = new();
    public decimal TotalAnnualTax { get; set; }
    public decimal MonthlyTax { get; set; }
    public decimal MonthlyNetSalary { get; set; }
}