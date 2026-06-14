namespace TaxService.Application.Interfaces;

public interface IAuditLogger
{
    Task LogAsync(string action, int employeeId, string details);
}