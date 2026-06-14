using EmployeeService.Api.Services;
using EmployeeService.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddGrpc();
builder.Services.AddDbContext<EmployeeDbContext>(opt =>
    opt.UseSqlServer("Server=KHAN\\MSSQLSERVER02;Database=HRTax_EmployeeDb;Trusted_Connection=True;TrustServerCertificate=True"));

builder.WebHost.ConfigureKestrel(o => o.ListenLocalhost(5001, lo => lo.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http2));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EmployeeDbContext>();
    db.Database.Migrate();
}

app.MapGrpcService<EmployeeGrpcService>();
app.Run();