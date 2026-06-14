using Microsoft.EntityFrameworkCore;
using TaxService.Application.Interfaces;

namespace TaxService.Infrastructure.Audit;

public class AuditLog
{
    public int Id { get; set; }
    public string Action { get; set; } = "";
    public int EmployeeId { get; set; }
    public string Details { get; set; } = "";
    public DateTime Timestamp { get; set; }
}

public class AuditDbContext : DbContext
{
    public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options) { }
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
}

public class EfAuditLogger : IAuditLogger
{
    private readonly AuditDbContext _db;
    public EfAuditLogger(AuditDbContext db) => _db = db;

    public async Task LogAsync(string action, int employeeId, string details)
    {
        _db.AuditLogs.Add(new AuditLog { Action = action, EmployeeId = employeeId, Details = details, Timestamp = DateTime.UtcNow });
        await _db.SaveChangesAsync();
    }
}