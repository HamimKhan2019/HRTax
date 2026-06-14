namespace TaxService.Application.Interfaces;

public interface ITaxEventPublisher
{
    Task PublishTaxCalculatedAsync(int employeeId, decimal totalTax);
}