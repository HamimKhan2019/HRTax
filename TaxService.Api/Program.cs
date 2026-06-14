using MediatR;
using Microsoft.EntityFrameworkCore;
using TaxService.Application.Interfaces;
using TaxService.Application.Queries;
using TaxService.Infrastructure.Audit;
using TaxService.Infrastructure.Grpc;
using TaxService.Infrastructure.Kafka;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("http://localhost:5050");

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetEmployeeTaxQueryHandler>());

builder.Services.AddDbContext<AuditDbContext>(opt =>
    opt.UseSqlServer("Server=KHAN\\MSSQLSERVER02;Database=HRTax_TaxAuditDb;Trusted_Connection=True;TrustServerCertificate=True"));

builder.Services.AddScoped<IEmployeeGrpcClient, EmployeeGrpcClient>();
builder.Services.AddScoped<IAuditLogger, EfAuditLogger>();
builder.Services.AddSingleton<ITaxEventPublisher, KafkaTaxEventPublisher>();

builder.Services.AddCors(o => o.AddPolicy("AllowAngular", p =>
    p.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader()));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AuditDbContext>();
    db.Database.Migrate();
}

app.UseCors("AllowAngular");
app.MapControllers();
app.Run();